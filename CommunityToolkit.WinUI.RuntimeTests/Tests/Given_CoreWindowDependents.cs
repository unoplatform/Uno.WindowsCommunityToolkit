// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using CommunityToolkit.WinUI.UI;
using CommunityToolkit.WinUI.UI.Helpers;
using CommunityToolkit.WinUI.UI.Triggers;
using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Uno.UI.RuntimeTests;
using Windows.UI.ViewManagement;

namespace CommunityToolkit.WinUI.RuntimeTests.Tests
{
    // On Uno Platform 7, Window.Current is set but has no CoreWindow, as in WinUI desktop apps.
    [TestClass]
    [RunsOnUIThread]
    public class Given_CoreWindowDependents
    {
        [TestMethod]
        public void When_ThemeListener_Is_Created_Then_It_Does_Not_Throw()
        {
            using var listener = new ThemeListener();
        }

        [TestMethod]
        public async Task When_MiddleClickScrolling_Is_Toggled_Then_It_Does_Not_Throw()
        {
            var scrollViewer = new ScrollViewer { Content = new Border { Height = 1000 } };
            await UIHelper.Load(scrollViewer);

            CommunityToolkit.WinUI.UI.ScrollViewerExtensions.SetEnableMiddleClickScrolling(scrollViewer, true);
            CommunityToolkit.WinUI.UI.ScrollViewerExtensions.SetEnableMiddleClickScrolling(scrollViewer, false);
        }

        // UIViewSettings.GetForCurrentView() is not implemented on Uno Platform.
        [TestMethod]
        public void When_UserInteractionModeStateTrigger_Mode_Is_Set_Then_It_Does_Not_Throw()
        {
            var trigger = new UserInteractionModeStateTrigger();

            trigger.InteractionMode = UserInteractionMode.Touch;
        }
    }
}
