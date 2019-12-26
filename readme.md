---
topic: sample
languages:
- csharp
products:
- windows
---

# Uno Platform port of Windows Community Toolkit

This port allows for [Uno-based](https://github.com/nventive/Uno) apps to use [Windows Community Toolkit](https://github.com/Microsoft/WindowsCommunityToolkit)
on Windows, iOS, Android and WebAssembly.

The following packages are available:
- [Uno.Microsoft.Toolkit](https://www.nuget.org/packages/Uno.Microsoft.Toolkit )
- [Uno.Microsoft.Toolkit.Parsers](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Parsers)
- [Uno.Microsoft.Toolkit.Services](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Services)
- [Uno.Microsoft.Toolkit.Notifications](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Notifications)
- [Uno.Microsoft.Toolkit.UWP](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.UWP)
- [Uno.Microsoft.Toolkit.Uwp.Services](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.Services)
- [Uno.Microsoft.Toolkit.Uwp.UI](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.UWP.UI)
- [Uno.Microsoft.Toolkit.Uwp.UI.Animations](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Animations)
- [Uno.Microsoft.Toolkit.Uwp.UI.Controls](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls)
- [Uno.Microsoft.Toolkit.Uwp.UI.Controls.Graph](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.UWP.UI.Controls.Graph)
- [Uno.Microsoft.Toolkit.Uwp.Connectivity](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.Connectivity)

# Windows Community Toolkit
The Windows Community Toolkit is a collection of helper functions, custom controls, and app services. It simplifies and demonstrates common developer patterns when building experiences for Windows 10.

## Build Status
| Target | Branch | Status |
| ------ | ------ | ------ |
| Pre-release beta testing | uno | [![Build Status](https://uno-platform.visualstudio.com/Uno%20Platform/_apis/build/status/Uno%20Platform/Uno.WindowsCommunityToolkit%20-%20CI?branchName=uno)](https://uno-platform.visualstudio.com/Uno%20Platform/_build/latest?definitionId=14&branchName=uno) |
<!--
| Target | Branch | Status | Recommended NuGet packages version |
| ------ | ------ | ------ | ------ |
| Pre-release beta testing | master | [![Build Status](https://uno-platform.visualstudio.com/Uno%20Platform/_apis/build/status/Uno%20Platform/Uno.WindowsCommunityToolkit%20-%20CI?branchName=uno)](https://uno-platform.visualstudio.com/Uno%20Platform/_build/latest?definitionId=14&branchName=uno) | [![MyGet](https://img.shields.io/dotnet.myget/uwpcommunitytoolkit/vpre/Microsoft.Toolkit.Uwp.svg)](https://dotnet.myget.org/gallery/uwpcommunitytoolkit) |
| Production | rel/3.0.0 | [![Build status](https://ci.appveyor.com/api/projects/status/o60lv2tt1nbtklw8/branch/rel/3.0.0?svg=true)](https://ci.appveyor.com/project/dotnetfoundation/uwpcommunitytoolkit/branch/rel/3.0.0) | [![NuGet](https://img.shields.io/nuget/v/Microsoft.Toolkit.Uwp.svg)](https://www.nuget.org/packages/Microsoft.Toolkit.Uwp/) |
-->

## Getting Started
Please read the [getting Started with the Windows Community Toolkit](https://docs.microsoft.com/windows/communitytoolkit/getting-started) page for more detailed information about using the toolkit.

## Documentation
All documentation for the toolkit is hosted on [Microsoft Docs](https://docs.microsoft.com/windows/communitytoolkit/). All API documentation can be found at the [.NET API Browser](https://docs.microsoft.com/en-us/dotnet/api/?term=microsoft.toolkit).

<!--
## Windows Community Toolkit Sample App
Want to see the toolkit in action before jumping into the code? Download and play with the [Windows Community Toolkit Sample App](https://www.microsoft.com/store/apps/9nblggh4tlcq) from the Store.
-->

## NuGet Packages
NuGet is a standard package manager for .NET applications which is built into Visual Studio. To open the UI, from your open solution, choose the *Tools* menu > *NuGet Package Manager* > *Manage NuGet packages for solution...* . Enter one of the package names below to search for it online.

Once you do a search, you should see a list similar to the one below (versions may be different, but names should be the same).

![nuget packages](githubresources/images/NugetPackages.png "Nuget Packages")

| NuGet Package Name | Description |
| --- | --- |
| Uno.Microsoft.Toolkit | .NET Standard NuGet package containing common code |
| Uno.Microsoft.Toolkit.Parsers | .NET Standard NuGet package containing cross-platform parsers, such as Markdown and RSS |
| Uno.Microsoft.Toolkit.Services | .NET Standard NuGet package containing cross-platform services |
| Uno.Microsoft.Toolkit.Uwp | Main NuGet package includes code only helpers such as Colors conversion tool, Storage file handling, a Stream helper class, etc. |
| Uno.Microsoft.Toolkit.Uwp.Notifications | Notifications Package - Generate tile, toast, and badge notifications for Windows 10 via code.  Includes intellisense support to avoid having to use the XML syntax. |
| Uno.Microsoft.Toolkit.Uwp.Notifications.Javascript | Notification Packages for JavaScript |
| Uno.Microsoft.Toolkit.Uwp.Services | Services Package - This NuGet package includes the service helpers for Facebook, LinkedIn, Microsoft Graph, Twitter and more |
| Uno.Microsoft.Toolkit.Uwp.UI | UI Packages - Brushes, XAML converters, Visual tree extensions, and other extensions and helpers for your XAML UI. |
| Uno.Microsoft.Toolkit.Uwp.UI.Animations | Animations and Composition behaviors such as Blur, Fade, Rotate, etc. |
| Uno.Microsoft.Toolkit.Uwp.UI.Controls | XAML Controls such as RadialGauge, RangeSelector, etc. |
| Uno.Microsoft.Toolkit.Uwp.UI.Controls.DataGrid | XAML DataGrid control |
| Uno.Microsoft.Toolkit.Uwp.Connectivity | API helpers such as BluetoothLEHelper and Networking |
| Uno.Microsoft.Toolkit.Uwp.DeveloperTools | XAML user controls and services to help developer building their app |

## <a name="supported"></a> Supported SDKs
* Fall Creators Update (16299)
* April 2018 Update (17134)
* October 2018 Update (17763)

## Features

### Animations
* [AnimationSet](https://docs.microsoft.com/windows/communitytoolkit/animations/AnimationSet/)
* [Blur](https://docs.microsoft.com/windows/communitytoolkit/animations/Blur/)
* [Composition Animations](https://docs.microsoft.com/windows/communitytoolkit/animations/CompositionAnimations/)
* [Connected Animations](https://docs.microsoft.com/windows/communitytoolkit/animations/ConnectedAnimations/)
* [ExpressionBuilder](https://docs.microsoft.com/windows/communitytoolkit/animations/Expressions/)
* [Fade](https://docs.microsoft.com/windows/communitytoolkit/animations/Fade/)
* [FadeHeader](https://docs.microsoft.com/windows/communitytoolkit/animations/FadeHeader/)
* [Implicit Animations](https://docs.microsoft.com/windows/communitytoolkit/animations/ImplicitAnimations/)
* [Light](https://docs.microsoft.com/windows/communitytoolkit/animations/Light/)
* [Offset](https://docs.microsoft.com/windows/communitytoolkit/animations/Offset/)
* [ReorderGrid](https://docs.microsoft.com/windows/communitytoolkit/animations/ReorderGrid/)
* [Rotate](https://docs.microsoft.com/windows/communitytoolkit/animations/Rotate/)
* [Saturation](https://docs.microsoft.com/windows/communitytoolkit/animations/Saturation/)
* [Scale](https://docs.microsoft.com/windows/communitytoolkit/animations/Scale/)

### Brushes
* [BackdropBlurBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/BackdropBlurBrush/)
* [BackdropGammaTransferBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/BackdropGammaTransferBrush/)
* [BackdropInvertBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/BackdropInvertBrush/)
* [BackdropSaturationBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/BackdropSaturationBrush/)
* [BackdropSepiaBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/BackdropSepiaBrush/)
* [ImageBlendBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/ImageBlendBrush/)
* [RadialGradientBrush](https://docs.microsoft.com/windows/communitytoolkit/brushes/RadialGradientBrush/)

### Controls
* [AdaptiveGridView](https://docs.microsoft.com/windows/communitytoolkit/controls/AdaptiveGridView/)
* [BladeView](https://docs.microsoft.com/windows/communitytoolkit/controls/BladeView/)
* [Carousel](https://docs.microsoft.com/windows/communitytoolkit/controls/Carousel/)
* [DataGrid](https://docs.microsoft.com/windows/communitytoolkit/controls/DataGrid/)
* [DockPanel](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/DockPanel/)
* [DropShadowPanel](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/DropShadowPanel/)
* [Expander](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/Expander/)
* [GridSplitter](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/GridSplitter/)
* [HeaderedContentControl](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/HeaderedContentControl/)
* [HeaderedItemsControl](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/HeaderedItemsControl/)
* [HeaderedTextBlock](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/HeaderedTextBlock/)
* [ImageEx](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/ImageEx/)
* [InAppNotification](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/InAppNotification/)
* [InfiniteCanvas](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/InfiniteCanvas/)
* [Loading](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/Loading/)
* [MarkdownTextBlock](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/MarkdownTextBlock/)
* [MasterDetailsView](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/MasterDetailsView/)
* [Menu](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/Menu/)
* [OrbitView](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/OrbitView/)
* [RadialGauge](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/RadialGauge/)
* [RadialProgressBar](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/RadialProgressBar/)
* [RangeSelector](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/RangeSelector/)
* [RotatorTile](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/RotatorTile/)
* [ScrollHeader](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/ScrollHeader/)
* [StaggeredPanel](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/StaggeredPanel/)
* [TabView](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/TabView/)
* [TextToolbar](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/TextToolbar/)
* [TileControl](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/TileControl/)
* [WrapPanel](https://docs.microsoft.com/windows/uwpcommunitytoolkit/controls/WrapPanel/)

### Developer Tools
* [AlignmentGrid](https://docs.microsoft.com/windows/communitytoolkit/developer-tools/AlignmentGrid/)
* [FocusTracker](https://docs.microsoft.com/windows/communitytoolkit/developer-tools/FocusTracker/)

### Extensions
* [FrameworkElementExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/FrameworkElementExtensions/)
* [HyperlinkExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/Hyperlink/)
* [ListViewExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/ListViewBase/)
* [LogicalTree](https://docs.microsoft.com/windows/communitytoolkit/extensions/LogicalTree/)
* [MouseCursor](https://docs.microsoft.com/windows/communitytoolkit/extensions/MouseCursor/)
* [ScrollViewerExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/ScrollViewerExtensions/)
* [SurfaceDialTextbox](https://docs.microsoft.com/windows/communitytoolkit/extensions/SurfaceDialTextboxHelper/)
* [TextBoxMask](https://docs.microsoft.com/windows/communitytoolkit/extensions/TextBoxMask/)
* [TextBoxRegex](https://docs.microsoft.com/windows/communitytoolkit/extensions/TextBoxRegex/)
* [ViewExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/ViewExtensions/)
* [VisualExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/VisualEx/)
* [VisualTree](https://docs.microsoft.com/windows/communitytoolkit/extensions/VisualTree/)
* [WebViewExtensions](https://docs.microsoft.com/windows/communitytoolkit/extensions/WebView/)

### Helpers
* [AdvancedCollectionView](https://docs.microsoft.com/windows/communitytoolkit/helpers/AdvancedCollectionView/)
* [BackgroundTaskHelper](https://docs.microsoft.com/windows/communitytoolkit/helpers/BackgroundTaskHelper/)
* [BindableValueHolder](https://docs.microsoft.com/windows/communitytoolkit/helpers/BindableValueHolder/)
* [BluetoothLEHelper](https://docs.microsoft.com/windows/communitytoolkit/helpers/BluetoothLEHelper/)
* [Colors](https://docs.microsoft.com/windows/communitytoolkit/helpers/Colors/)
* [Converters](https://docs.microsoft.com/windows/communitytoolkit/helpers/Converters/)
* [DeepLinkParsers](https://docs.microsoft.com/windows/communitytoolkit/helpers/DeepLinkParsers/)
* [DispatcherHelper](https://docs.microsoft.com/windows/communitytoolkit/helpers/DispatcherHelper/)
* [HttpHelper](https://docs.microsoft.com/windows/communitytoolkit/helpers/HttpHelper/)
* [ImageCache](https://docs.microsoft.com/windows/communitytoolkit/helpers/ImageCache/)
* [Incremental Loading Collection](https://docs.microsoft.com/windows/communitytoolkit/helpers/IncrementalLoadingCollection/)
* [NetworkHelper](https://docs.microsoft.com/windows/communitytoolkit/helpers/NetworkHelper/)
* [Object Storage](https://docs.microsoft.com/windows/communitytoolkit/helpers/ObjectStorage/)
* [PrintHelper](https://docs.microsoft.com/windows/communitytoolkit/helpers/PrintHelper/)
* [StorageFiles](https://docs.microsoft.com/windows/communitytoolkit/helpers/StorageFiles/)
* [Streams](https://docs.microsoft.com/windows/communitytoolkit/helpers/Streams/)
* [SystemInformation](https://docs.microsoft.com/windows/communitytoolkit/helpers/SystemInformation/)
* [ThemeListener](https://docs.microsoft.com/windows/communitytoolkit/helpers/ThemeListener/)
* [WeakEventListener](https://docs.microsoft.com/windows/communitytoolkit/helpers/WeakEventListener/)

### Services
* [Facebook](https://docs.microsoft.com/windows/communitytoolkit/services/Facebook/)
* [LinkedIn](https://docs.microsoft.com/windows/communitytoolkit/services/Linkedin/)
* [Microsoft Graph](https://docs.microsoft.com/windows/communitytoolkit/services/MicrosoftGraph/)
* [Microsoft Translator Service](https://docs.microsoft.com/windows/communitytoolkit/services/MicrosoftTranslator/)
* [OneDrive](https://docs.microsoft.com/windows/communitytoolkit/services/OneDrive/)
* [Twitter](https://docs.microsoft.com/windows/communitytoolkit/services/Twitter/)

### Parsers
* [Markdown Parser](https://docs.microsoft.com/windows/communitytoolkit/parsers/MarkdownParser/)
* [RSS Parser](https://docs.microsoft.com/windows/communitytoolkit/parsers/RSSParser/)

### Notifications
* [Tiles](https://blogs.msdn.microsoft.com/tiles_and_toasts/2015/06/30/adaptive-tile-templates-schema-and-documentation/)
* [Toasts](https://blogs.msdn.microsoft.com/tiles_and_toasts/2015/07/02/adaptive-and-interactive-toast-notifications-for-windows-10/)

## Feedback and Requests
Please use [GitHub Issues](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/issues) for bug reports and feature requests.
For general questions and support, please use [Stack Overflow](https://stackoverflow.com/questions/tagged/uno-platform+windows-community-toolkit) where questions should be tagged with the tag `uno-platform` and `windows-community-toolkit`.

## Contributing
Do you want to contribute? Here are our [contribution guidelines](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/blob/uno/contributing.md).

## Principles
* Principle **#1**: The toolkit will be kept simple.
* Principle **#2**: As soon as a comparable feature is available in the Windows SDK for Windows 10, it will be marked as deprecated.
* Principle **#3**: All features will be supported for two Windows SDK for Windows 10 release cycles or until another principle supersedes it.

This project has adopted the code of conduct defined by the [Contributor Covenant](http://contributor-covenant.org/)
to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](http://dotnetfoundation.org/code-of-conduct).

## Uno Platform
This project is supported by the [Uno Platform](https://platform.uno).
