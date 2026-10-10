// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Controls;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Media;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uno.UI.RuntimeTests;

// WinUI declares its own Expander and ExpandDirection, which has no Left or Right.
using ExpandDirection = CommunityToolkit.WinUI.UI.Controls.ExpandDirection;
using Expander = CommunityToolkit.WinUI.UI.Controls.Expander;

namespace CommunityToolkit.WinUI.RuntimeTests.Tests
{
    [TestClass]
    [RunsOnUIThread]
    public class Given_LayoutControls
    {
        // An Uno-only workaround left Child null on Uno Platform 7, so the control measured 0x0.
        [TestMethod]
        public async Task When_LayoutTransformControl_Then_Child_Is_Measured()
        {
            var control = new LayoutTransformControl { Child = new Border { Width = 100, Height = 50 } };

            await UIHelper.Load(control);

            Assert.AreEqual(100, control.ActualWidth, 0.5);
            Assert.AreEqual(50, control.ActualHeight, 0.5);
        }

        [TestMethod]
        public async Task When_LayoutTransformControl_Rotated_Then_Bounds_Are_Swapped()
        {
            var control = new LayoutTransformControl
            {
                Child = new Border { Width = 100, Height = 50 },
                Transform = new RotateTransform { Angle = 90 },
            };

            await UIHelper.Load(control);

            Assert.AreEqual(50, control.ActualWidth, 0.5);
            Assert.AreEqual(100, control.ActualHeight, 0.5);
        }

        // The header sits in a LayoutTransformControl and disappeared with it.
        [TestMethod]
        [DataRow(ExpandDirection.Down)]
        [DataRow(ExpandDirection.Right)]
        public async Task When_Expander_Expanded_Then_Header_And_Content_Are_Shown(ExpandDirection direction)
        {
            var content = new TextBlock { Text = "content" };
            var expander = new Expander { Header = "header", IsExpanded = true, ExpandDirection = direction, Content = content };

            await UIHelper.Load(expander);
            await UIHelper.WaitForIdle();

            var header = expander.FindDescendant<ToggleButton>();
            Assert.IsNotNull(header);
            Assert.IsTrue(header.ActualWidth > 0 && header.ActualHeight > 0, $"Header is {header.ActualWidth}x{header.ActualHeight}");
            Assert.IsTrue(content.ActualWidth > 0 && content.ActualHeight > 0, $"Content is {content.ActualWidth}x{content.ActualHeight}");
        }

        // Its content binding sat under the removed xamarin: XAML prefix.
        [TestMethod]
        public async Task When_HeaderedContentControl_Then_Content_Is_Shown()
        {
            var content = new TextBlock { Text = "content" };
            var control = new HeaderedContentControl { Header = "header", Content = content };

            await UIHelper.Load(control);

            Assert.IsTrue(content.ActualWidth > 0 && content.ActualHeight > 0, $"Content is {content.ActualWidth}x{content.ActualHeight}");
        }

        // Its items panel sat under the removed xamarin: XAML prefix, which left it empty on Uno.
        [TestMethod]
        public async Task When_BladeView_Then_Blades_Are_Realized_In_A_StackPanel()
        {
            var first = new BladeItem { Header = "first", Width = 200 };
            var second = new BladeItem { Header = "second", Width = 200 };
            var bladeView = new BladeView { Height = 200, Items = { first, second } };

            await UIHelper.Load(bladeView);
            await UIHelper.WaitForIdle();

            Assert.IsInstanceOfType<StackPanel>(VisualTreeHelper.GetParent(first));
            Assert.IsTrue(first.ActualWidth > 0, "The first blade is not realized");
            Assert.IsTrue(second.ActualWidth > 0, "The second blade is not realized");
        }
    }
}
