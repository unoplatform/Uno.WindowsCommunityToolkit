// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uno.UI.RuntimeTests;
using Windows.Devices.Input;
using Windows.Foundation;

namespace CommunityToolkit.WinUI.RuntimeTests.Tests
{
    [TestClass]
    [RunsOnUIThread]
    public class Given_GridSplitter
    {
        // Uno Platform 7 reports the distance travelled before a manipulation is recognized only in
        // ManipulationStarted, so a splitter that sums the deltas lags the pointer by that threshold.
        [TestMethod]
        [InjectedPointer(PointerDeviceType.Mouse)]
        [Ignore("Injected mouse input does not reach the app with Uno.UI.RuntimeTests.Engine 3.0.0-dev.2 on Uno Platform 7.0.0-dev.1666: the press is never delivered and moves land offset.")]
        public async Task When_Dragged_Then_Column_Follows_The_Pointer()
        {
            var leftColumn = new ColumnDefinition { Width = new GridLength(200) };
            var splitter = new GridSplitter { ResizeDirection = GridSplitter.GridResizeDirection.Columns };
            Grid.SetColumn(splitter, 1);
            var grid = new Grid
            {
                Width = 600,
                Height = 100,
                ColumnDefinitions = { leftColumn, new ColumnDefinition { Width = new GridLength(16) }, new ColumnDefinition() },
                Children = { splitter },
            };

            await UIHelper.Load(grid);

            var injector = InputInjectorHelper.TryGetCurrent() ?? throw new InvalidOperationException("Input injection is not available.");
            var from = splitter.TransformToVisual(null).TransformPoint(new Point(splitter.ActualWidth / 2, splitter.ActualHeight / 2));
            injector.DragCoordinates(from, new Point(from.X + 100, from.Y));
            await UIHelper.WaitForIdle();

            Assert.AreEqual(300, leftColumn.ActualWidth, 1);
        }
    }
}
