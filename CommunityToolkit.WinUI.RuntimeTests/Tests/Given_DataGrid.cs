using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uno.UI.RuntimeTests;

namespace CommunityToolkit.WinUI.RuntimeTests.Tests
{
    [TestClass]
    [RunsOnUIThread]
    public class Given_DataGrid
    {
        [TestMethod]
        public async Task When_ItemsSource_Then_Rows_Are_Realized()
        {
            var grid = await LoadGrid();

            var rows = grid.FindDescendants().OfType<DataGridRow>().Count(r => r.ActualHeight > 0);
            Assert.IsTrue(rows > 0, "No row was realized");
        }

        // An Uno-only UpdateDisplayedRows call after MeasureOverride was removed; growing the viewport must still
        // realize the rows that become visible, with no blank band at the bottom.
        [TestMethod]
        public async Task When_Viewport_Grows_Then_New_Rows_Are_Realized()
        {
            var grid = new DataGrid
            {
                AutoGenerateColumns = true,
                ItemsSource = Enumerable.Range(0, 40).Select(i => new Person { Name = $"Person {i}", Age = 20 + i }).ToList(),
            };
            var host = new Border { Height = 200, Width = 400, Child = grid };

            await UIHelper.Load(host);
            await UIHelper.WaitForIdle();

            var rowsBefore = VisibleRows(grid);
            Assert.IsTrue(rowsBefore > 0, "No row was realized");

            host.Height = 500;
            await UIHelper.WaitForIdle();

            var rowsAfter = VisibleRows(grid);
            Assert.IsTrue(rowsAfter > rowsBefore, $"Growing the viewport realized no new row ({rowsBefore} before, {rowsAfter} after)");

            // The last visible row must reach the bottom of the grid, allowing for one partial row.
            var lastRow = grid.FindDescendants().OfType<DataGridRow>().Where(r => r.ActualHeight > 0).OrderByDescending(r => RowBottom(grid, r)).First();
            var gap = grid.ActualHeight - RowBottom(grid, lastRow);
            Assert.IsTrue(gap < lastRow.ActualHeight, $"A blank band of {gap}px is left under the last row");
        }

        private static int VisibleRows(DataGrid grid)
            => grid.FindDescendants().OfType<DataGridRow>().Count(r => r.ActualHeight > 0);

        private static double RowBottom(DataGrid grid, DataGridRow row)
            => row.TransformToVisual(grid).TransformPoint(new Windows.Foundation.Point(0, row.ActualHeight)).Y;

        // ReadOnlyAttribute used to be ignored on Uno, unlike on WinAppSDK.
        [TestMethod]
        public async Task When_Property_Is_ReadOnly_Then_Its_Column_Cannot_Be_Edited()
        {
            var grid = await LoadGrid();

            grid.SelectedIndex = 0;
            grid.CurrentColumn = grid.Columns.Single(c => (string)c.Header == nameof(Person.Age));

            Assert.IsFalse(grid.BeginEdit());
        }

        // An Uno-only focus workaround (uno#2895) left the editing element unfocused on Uno Platform 7.
        [TestMethod]
        public async Task When_Editing_A_Cell_Then_The_Editor_Has_Focus()
        {
            var grid = await LoadGrid();
            grid.Focus(FocusState.Programmatic);
            grid.SelectedIndex = 0;
            grid.CurrentColumn = grid.Columns.Single(c => (string)c.Header == nameof(Person.Name));

            Assert.IsTrue(grid.BeginEdit());
            await UIHelper.WaitForIdle();

            var editor = grid.FindDescendant<TextBox>();
            Assert.IsNotNull(editor);
            Assert.AreSame(editor, FocusManager.GetFocusedElement(grid.XamlRoot));

            grid.CancelEdit();
        }

        // The source holds the weak listeners of the grid and of the collection view the grid wraps a list in.
        // Their closures used to capture their owner, so a long-lived source (e.g. a view model's collection)
        // kept every grid ever bound to it alive.
        [TestMethod]
        public async Task When_ItemsSource_Is_A_Long_Lived_List_Then_It_Does_Not_Keep_The_Grid_Alive()
        {
            var items = CreateRows();

            var (grid, view) = await LoadAndUnloadGrid(items);

            Assert.IsNotNull(view, "The DataGrid did not wrap the list in a collection view");
            await AssertCollected(grid, "The DataGrid");
            await AssertCollected(view, "The collection view the DataGrid wrapped the list in");

            GC.KeepAlive(items);
        }

        [TestMethod]
        public async Task When_ItemsSource_Is_A_Long_Lived_CollectionView_Then_It_Does_Not_Keep_The_Grid_Alive()
        {
            var items = CreateRows();
            var source = new AdvancedCollectionView(items);

            var (grid, _) = await LoadAndUnloadGrid(source);

            await AssertCollected(grid, "The DataGrid");

            GC.KeepAlive(source);
            GC.KeepAlive(items);
        }

        // The mouse wheel used to jump a fixed 30px per notch. It now moves like the ScrollViewer: 15% of the viewport per notch,
        // animated, with rapid notches adding up to one target. Injected mouse input is unreliable with this engine (presses are lost
        // and moves land offset, see Given_GridSplitter), so the tests call the grid's wheel handling directly.
        [TestMethod]
        public async Task When_Mouse_Wheel_Notch_Then_Scrolls_A_Share_Of_The_Viewport_With_An_Animation()
        {
            var grid = await LoadGrid(rows: 200);
            var step = GetOffset(grid, "CellsHeight") * 0.15;

            Assert.IsTrue(ScrollForMouseWheel(grid, -120));
            Assert.AreEqual(0, GetOffset(grid, "VerticalOffset"), "The notch scrolled at once instead of animating");

            await WaitForWheelMotion();
            Assert.AreEqual(step, GetOffset(grid, "VerticalOffset"), 1);
        }

        [TestMethod]
        public async Task When_Rapid_Mouse_Wheel_Notches_Then_They_Add_Up()
        {
            var grid = await LoadGrid(rows: 200);
            var step = GetOffset(grid, "CellsHeight") * 0.15;

            for (var i = 0; i < 6; i++)
            {
                Assert.IsTrue(ScrollForMouseWheel(grid, -120));
            }

            await WaitForWheelMotion();
            Assert.AreEqual(6 * step, GetOffset(grid, "VerticalOffset"), 1);

            // The rows under the new offset are realized.
            Assert.IsTrue(grid.FindDescendants().OfType<DataGridRow>().Any(r => r.ActualHeight > 0 && r.GetIndex() > 5), "No row past the first ones was realized");
        }

        [TestMethod]
        public async Task When_Mouse_Wheel_Turned_Up_At_The_Top_Then_It_Is_Left_To_A_Parent()
        {
            var grid = await LoadGrid(rows: 200);

            Assert.IsFalse(ScrollForMouseWheel(grid, 120));
        }

        [TestMethod]
        public async Task When_Mouse_Wheel_Is_Sideways_Then_Scrolls_Horizontally()
        {
            var grid = await LoadGrid(rows: 20);
            grid.Width = 150;
            grid.Columns[0].Width = new DataGridLength(400);
            await UIHelper.WaitForIdle();

            var step = GetOffset(grid, "CellsWidth") * 0.15;

            // Turning the wheel toward the user with Shift scrolls to the right, as it does a ScrollViewer.
            Assert.IsTrue(ScrollForMouseWheel(grid, -120, isForHorizontalScroll: true));
            await WaitForWheelMotion();

            Assert.AreEqual(step, GetOffset(grid, "HorizontalOffset"), 1);
            Assert.AreEqual(0, GetOffset(grid, "VerticalOffset"));
        }

        private static bool ScrollForMouseWheel(DataGrid grid, double delta, bool isForHorizontalScroll = false)
        {
            var method = typeof(DataGrid).GetMethod("ScrollForMouseWheel", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(method, "DataGrid.ScrollForMouseWheel was not found");
            return (bool)method.Invoke(grid, new object[] { delta, isForHorizontalScroll, false })!;
        }

        private static double GetOffset(DataGrid grid, string name)
        {
            var property = typeof(DataGrid).GetProperty(name, BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(property, $"DataGrid.{name} was not found");
            return (double)property.GetValue(grid)!;
        }

        // The motion runs for 220ms from its first frame.
        private static async Task WaitForWheelMotion()
        {
            await Task.Delay(500);
            await UIHelper.WaitForIdle();
        }

        private static ObservableCollection<Row> CreateRows()
            => new(Enumerable.Range(0, 20).Select(i => new Row { Name = $"Row {i}" }));

        private static async Task AssertCollected(WeakReference reference, string what)
        {
            for (var i = 0; i < 10 && reference.IsAlive; i++)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                await UIHelper.WaitForIdle();
            }

            Assert.IsFalse(reference.IsAlive, $"{what} is still alive after the grid was unloaded");
        }

        // Kept out of line so that no local of the caller roots the grid. Returns the grid and, when the grid
        // wrapped the source in a collection view of its own, that view (read through the internal DataConnection).
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static async Task<(WeakReference Grid, WeakReference? View)> LoadAndUnloadGrid(IEnumerable itemsSource)
        {
            var grid = new DataGrid
            {
                Height = 200,
                AutoGenerateColumns = true,
                ItemsSource = itemsSource,
            };

            await UIHelper.Load(grid);
            await UIHelper.WaitForIdle();
            Assert.IsTrue(VisibleRows(grid) > 0, "No row was realized");

            var dataConnectionProperty = typeof(DataGrid).GetProperty("DataConnection", BindingFlags.Instance | BindingFlags.NonPublic);
            Assert.IsNotNull(dataConnectionProperty, "DataGrid.DataConnection was not found");
            var dataConnection = dataConnectionProperty.GetValue(grid);
            Assert.IsNotNull(dataConnection, "DataGrid.DataConnection is null");
            var dataSourceProperty = dataConnection.GetType().GetProperty("DataSource");
            Assert.IsNotNull(dataSourceProperty, "DataGridDataConnection.DataSource was not found");
            var dataSource = dataSourceProperty.GetValue(dataConnection);
            var view = dataSource is not null && !ReferenceEquals(dataSource, itemsSource) ? new WeakReference(dataSource) : null;

            UIHelper.Content = null;
            await UIHelper.WaitForIdle();

            return (new WeakReference(grid), view);
        }

        private static async Task<DataGrid> LoadGrid(int rows = 20)
        {
            var grid = new DataGrid
            {
                Height = 200,
                AutoGenerateColumns = true,
                ItemsSource = Enumerable.Range(0, rows).Select(i => new Person { Name = $"Person {i}", Age = 20 + i }).ToList(),
            };

            await UIHelper.Load(grid);
            await UIHelper.WaitForIdle();

            return grid;
        }

        public sealed class Row : INotifyPropertyChanged
        {
            private string _name = string.Empty;

            public event PropertyChangedEventHandler? PropertyChanged;

            public string Name
            {
                get => _name;
                set
                {
                    _name = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
                }
            }
        }

        public sealed class Person
        {
            public string Name { get; set; } = string.Empty;

            [ReadOnly(true)]
            public int Age { get; set; }
        }
    }
}
