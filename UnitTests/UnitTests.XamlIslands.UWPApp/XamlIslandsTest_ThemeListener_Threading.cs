// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using CommunityToolkit.WinUI;
using CommunityToolkit.WinUI.UI.Helpers;
using Microsoft.UI.Xaml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests.XamlIslands.UWPApp
{
    [STATestClass]
    public partial class XamlIslandsTest_ThemeListener_Threading
    {
        private TaskCompletionSource<object> _taskCompletionSource;
        private ThemeListener _themeListener = null;

        [TestInitialize]
        public Task Init()
        {
            return App.Dispatcher.EnqueueAsync(() =>
            {
                _taskCompletionSource = new TaskCompletionSource<object>();

                _themeListener = new ThemeListener
                {
                    CurrentTheme = Application.Current.RequestedTheme
                };
                _themeListener.ThemeChanged += (s) =>
                {
                    _taskCompletionSource.TrySetResult(null);
                };

                _themeListener.CurrentTheme = Application.Current.RequestedTheme == ApplicationTheme.Light ? ApplicationTheme.Dark : ApplicationTheme.Light;
            });
        }

        [TestCleanup]
        public Task Cleanup()
        {
            return App.Dispatcher.EnqueueAsync(() => _themeListener.Dispose());
        }

        [TestMethod]
        public async Task ThemeListenerCanBeCreatedAndDisposedForCurrentWindowAsync()
        {
            await App.Dispatcher.EnqueueAsync(() =>
            {
                using var listener = new ThemeListener();
                Assert.AreEqual(Application.Current.RequestedTheme, listener.CurrentTheme);
                Assert.IsNotNull(listener.DispatcherQueue);
                listener.Dispose();
            });
        }

        [TestMethod]
        public async Task ThemeListenerDispatcherTestAsync()
        {
            await _themeListener.OnThemePropertyChangedAsync();

            await _taskCompletionSource.Task;
        }

        [TestMethod]
        public async Task ThemeListenerDispatcherTestFromOtherThreadAsync()
        {
            await Task.Run(async () =>
            {
                await _themeListener.OnThemePropertyChangedAsync();
            });
            await _taskCompletionSource.Task;
        }
    }
}