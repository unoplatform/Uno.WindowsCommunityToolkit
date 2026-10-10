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
