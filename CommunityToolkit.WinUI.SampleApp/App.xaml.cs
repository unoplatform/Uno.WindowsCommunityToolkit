// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CommunityToolkit.WinUI.Helpers;
using CommunityToolkit.WinUI.SampleApp.Common;
using CommunityToolkit.WinUI.SampleApp.Styles;
using Microsoft.UI.Xaml;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.DataTransfer;
using Windows.System.Profile;
using Windows.UI.ViewManagement;

#if !HAS_UNO
using WinRT;

namespace CommunityToolkit.WinUI.SampleApp
{
    public sealed partial class App : Application
    {
        private MainWindow _window;

        public IntPtr WindowHandle { get; private set; }

        [ComImport]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        [Guid("EECDBF0E-BAE9-4CB6-A68E-9598E1CB57BB")]
        internal interface IWindowNative
        {
            IntPtr WindowHandle { get; }
        }

        [ComImport]
        [Guid("3E68D4BD-7135-4D10-8018-9FB6D9F33FA1")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IInitializeWithWindow
        {
            void Initialize(IntPtr hwnd);
        }

        public App()
        {
            InitializeComponent();

            // Suspending += OnSuspending;
        }

        /*
        protected override async void OnActivated(IActivatedEventArgs args)
        {
            RunAppInitialization(null);

            if (args.Kind == ActivationKind.Protocol)
            {
                try
                {
                    // Launching via protocol link
                    var parser = DeepLinkParser.Create(args);
                    var targetSample = await Sample.FindAsync(parser.Root, parser["sample"]);
                    if (targetSample != null)
                    {
                        Shell.Current?.NavigateToSample(targetSample);
                    }
                }
                catch (Exception ex)
                {
                    global::System.Diagnostics.Debug.WriteLine($"Error processing protocol launch: {ex.ToString()}");
                }
            }
        }
        */

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs e)
        {
            base.OnLaunched(e);

            IActivatedEventArgs activatedArgs = AppInstance.GetActivatedEventArgs();

            if (activatedArgs.PreviousExecutionState != ApplicationExecutionState.Running
                && activatedArgs.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                RunAppInitialization(e?.Arguments);
            }

            SystemInformation.Instance.TrackAppUse(activatedArgs);
        }

        /*
        /// <summary>
        /// Event fired when a Background Task is activated (in Single Process Model)
        /// </summary>
        /// <param name="args">Arguments that describe the BackgroundTask activated</param>
        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            base.OnBackgroundActivated(args);

            var deferral = args.TaskInstance.GetDeferral();

            switch (args.TaskInstance.Task.Name)
            {
                case Constants.TestBackgroundTaskName:
                    new TestBackgroundTask().Run(args.TaskInstance);
                    break;
            }

            deferral.Complete();
        }
        */

        private void RunAppInitialization(string launchParameters)
        {
            ThemeInjector.InjectThemeResources(Resources);

            // Go full screen on Xbox
            if (AnalyticsInfo.VersionInfo.GetDeviceFormFactor() == DeviceFormFactor.Xbox)
            {
                Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            }

            // Initialize the constant for the app display name, used for tile and toast previews
            if (Constants.ApplicationDisplayName == null)
            {
                // Constants.ApplicationDisplayName = (await Package.Current.GetAppListEntriesAsync())[0].DisplayInfo.DisplayName;
            }

            // Check if the Cache is Latest, wipe if not.
            Sample.EnsureCacheLatest();

            _window = new MainWindow(launchParameters);

            IWindowNative windowWrapper = _window.As<IWindowNative>();
            WindowHandle = windowWrapper.WindowHandle;

            _window.Activate();
        }

        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            try
            {
                // Here we flush the Clipboard to make sure content in clipboard to remain available
                // after the application shuts down.
                Clipboard.Flush();
            }
            catch (Exception)
            {
                // ignore
            }

            try
            {
                await Task.Delay(2000);
            }
            catch
            {
                // ignore
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
#else
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
using Microsoft.Extensions.Logging;
using Uno.Extensions;
using Uno.Logging;
using Windows.Foundation.Metadata;

namespace CommunityToolkit.WinUI.SampleApp
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    public sealed partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
#if DEBUG
            ConfigureFilters(LogExtensionPoint.AmbientLoggerFactory);
#endif

            InitializeComponent();
            Suspending += OnSuspending;
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await RunAppInitialization(null);

            if (args.Kind == ActivationKind.Protocol)
            {
                try
                {
                    // Launching via protocol link
                    var parser = DeepLinkParser.Create(args);
                    var targetSample = await Sample.FindAsync(parser.Root, parser["sample"]);
                    if (targetSample != null)
                    {
                        Shell.Current?.NavigateToSample(targetSample);
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Error processing protocol launch: {ex.ToString()}");
                }
            }
        }

        /// <summary>
        /// Invoked when the application is launched normally by the end user.  Other entry points
        /// will be used such as when the application is launched to open a specific file.
        /// </summary>
        /// <param name="e">Details about the launch request and process.</param>
        protected override async void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs e)
        {
            ApplicationView.GetForCurrentView().SetPreferredMinSize(new Windows.Foundation.Size(500, 500));

            if (e.UWPLaunchActivatedEventArgs.PrelaunchActivated)
            {
                return;
            }

            if (e.UWPLaunchActivatedEventArgs.PreviousExecutionState != ApplicationExecutionState.Running
                && e.UWPLaunchActivatedEventArgs.PreviousExecutionState != ApplicationExecutionState.Suspended)
            {
                await RunAppInitialization(e?.Arguments);
            }

            // UNO TODO SystemInformation.Instance.TrackAppUse(e);
        }

        /*
        /// <summary>
        /// Event fired when a Background Task is activated (in Single Process Model)
        /// </summary>
        /// <param name="args">Arguments that describe the BackgroundTask activated</param>
        protected override void OnBackgroundActivated(BackgroundActivatedEventArgs args)
        {
            base.OnBackgroundActivated(args);

            var deferral = args.TaskInstance.GetDeferral();

            switch (args.TaskInstance.Task.Name)
            {
                case Constants.TestBackgroundTaskName:
                    new TestBackgroundTask().Run(args.TaskInstance);
                    break;
            }

            deferral.Complete();
        }
        */

        private async System.Threading.Tasks.Task RunAppInitialization(string launchParameters)
        {
            ThemeInjector.InjectThemeResources(Application.Current.Resources);

            // Go full screen on Xbox
            if (AnalyticsInfo.VersionInfo.GetDeviceFormFactor() == DeviceFormFactor.Xbox)
            {
                Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().SetDesiredBoundsMode(ApplicationViewBoundsMode.UseCoreWindow);
            }

#if !HAS_UNO
            // Initialize the constant for the app display name, used for tile and toast previews
            if (Constants.ApplicationDisplayName == null)
            {
                Constants.ApplicationDisplayName = (await Package.Current.GetAppListEntriesAsync())[0].DisplayInfo.DisplayName;
            }
#endif

            // Check if the Cache is Latest, wipe if not.
            Sample.EnsureCacheLatest();

            Microsoft.UI.Xaml.Controls.Frame rootFrame = Microsoft.UI.Xaml.Window.Current.Content as Microsoft.UI.Xaml.Controls.Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Microsoft.UI.Xaml.Controls.Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                // Place the frame in the current Window
                Microsoft.UI.Xaml.Window.Current.Content = rootFrame;
            }

            if (rootFrame.Content == null)
            {
                // When the navigation stack isn't restored navigate to the first page,
                // configuring the new page by passing required information as a navigation
                // parameter
#if HAS_UNO
                if (!string.IsNullOrEmpty(launchParameters))
                {
                    launchParameters = "?" + launchParameters;
                }
#endif

                rootFrame.Navigate(typeof(Shell), launchParameters);
            }

            // Ensure the current window is active
            Microsoft.UI.Xaml.Window.Current.Activate();
        }

        /// <summary>
        /// Invoked when Navigation to a certain page fails
        /// </summary>
        /// <param name="sender">The Frame which failed navigation</param>
        /// <param name="e">Details about the navigation failure</param>
        private void OnNavigationFailed(object sender, Microsoft.UI.Xaml.Navigation.NavigationFailedEventArgs e)
        {
            throw new Exception($"Failed to load Page {e.SourcePageType}: {e.Exception}");
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        private async void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            // TODO: Save application state and stop any background activity
            try
            {
                // Here we flush the Clipboard to make sure content in clipboard to remain available
                // after the application shuts down.
                Clipboard.Flush();
            }
            catch (Exception)
            {
                // ignore
            }

            try
            {
                await Task.Delay(2000);
            }
            catch
            {
                // ignore
            }
            finally
            {
                deferral.Complete();
            }
        }

        static void ConfigureFilters(ILoggerFactory factory)
        {
#if HAS_UNO
            System.Threading.Tasks.TaskScheduler.UnobservedTaskException += (s, e) => typeof(App).Log().Error("UnobservedTaskException", e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => typeof(App).Log().Error("UnhandledException", e.ExceptionObject as Exception);
#endif

            factory
                .WithFilter(new FilterLoggerSettings
                    {
                        { "Uno", LogLevel.Warning },
                        { "Windows", LogLevel.Warning },
                        { "Microsoft", LogLevel.Warning },

                        // { "SampleControl.Presentation", LogLevel.Debug },

                        // Generic Xaml events
                        // { "Windows.UI.Xaml", LogLevel.Debug },

                    // { "Uno.UI.Controls.AsyncValuePresenter", LogLevel.Debug },
                    // { "Uno.UI.Controls.IfDataContext", LogLevel.Debug },
                     // { "Windows.UI.Xaml.FrameworkElement", LogLevel.Debug },
                    // { "Windows.UI.Xaml.UIElement", LogLevel.Debug },
                    // { "Windows.UI.Xaml.Controls.SinglelineTextBoxView", LogLevel.Debug },

                    // Layouter specific messages
                    // { "Windows.UI.Xaml.Controls", LogLevel.Debug },
                    // { "Windows.UI.Xaml.Controls.Layouter", LogLevel.Debug },
                    // { "Windows.UI.Xaml.Controls.Panel", LogLevel.Debug },

                    // Binding related messages
                     // { "Windows.UI.Xaml.Data", LogLevel.Debug },
                    // { "Windows.UI.Xaml.DependencyObjectStore", LogLevel.Debug },
                     // { "Uno.UI.DataBinding.BindingPropertyHelper", LogLevel.Debug },

					// Binder memory references tracking
					// { "ReferenceHolder", LogLevel.Debug },
				}
                )
#if !NETFX_CORE
                .AddConsole(LogLevel.Debug)
#endif
                ;

#if HAS_UNO
            Uno.UI.Adapter.Microsoft.Extensions.Logging.LoggingAdapter.Initialize();
#endif
        }
    }
}
#endif