// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
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

        private static async Task<DataGrid> LoadGrid()
        {
            var grid = new DataGrid
            {
                Height = 200,
                AutoGenerateColumns = true,
                ItemsSource = Enumerable.Range(0, 20).Select(i => new Person { Name = $"Person {i}", Age = 20 + i }).ToList(),
            };

            await UIHelper.Load(grid);
            await UIHelper.WaitForIdle();

            return grid;
        }

        public sealed class Person
        {
            public string Name { get; set; } = string.Empty;

            [ReadOnly(true)]
            public int Age { get; set; }
        }
    }
}
