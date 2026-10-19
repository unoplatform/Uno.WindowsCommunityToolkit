# Uno Platform port of Windows Community Toolkit version 7.x

This port allows for [Uno Platform based](https://github.com/unoplatform/uno) apps to use [Windows Community Toolkit version 7.x](https://github.com/Microsoft/WindowsCommunityToolkit) for WinUI 3
on Windows, iOS, macOS, Android, WebAssembly, and Linux.

> [!IMPORTANT]
> Note that this [Uno Platform fork](https://github.com/unoplatform/Uno.WindowsCommunityToolkit) of the Windows Community Toolkit is for **[version 7.x](xref:Uno.Development.CommunityToolkit.v7) only**.
> If you update to version 8.x, Uno Platform support is now available out of the box. You can remove the Uno Windows Community Toolkit references and all `Condition` statements around the packages.
> For more details on how to use Windows Community Toolkit, complete details are available [here](https://aka.platform.uno/install-uno-community-toolkit).

## Versions

Pick the package version that matches the Uno Platform version of your app:

| Packages | Uno Platform | Target frameworks | Branch |
|---|---|---|---|
| 7.2.x | 7.0 and later | `net10.0`, used by every Uno Platform head | [`uno`](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/tree/uno) |
| 7.1.2xx | 5.x and 6.x | `net7.0`, `net7.0-android`, `net7.0-ios`, `net7.0-maccatalyst`, `net7.0-macos` | [`servicing/7.1`](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/tree/servicing/7.1) for dev builds, [`unorel/winui/7.1.200`](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/tree/unorel/winui/7.1.200) for stable releases |
| 7.1.1xx | 4.x | Xamarin and `net6.0` targets | [`unorel/winui/7.1`](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/tree/unorel/winui/7.1) |

Until Uno Platform 7.0 is released, 7.2 is published to nuget.org as `7.2.0-dev.N` prereleases. They depend on `Uno.WinUI` 7.0.0-dev.2347 or later, so the app needs an Uno.Sdk that ships at least that version: Uno.Sdk 7.0.0-dev.81 (`Uno.WinUI` 7.0.0-dev.2347) works, while 7.0.0-dev.67 (`Uno.WinUI` 7.0.0-dev.2100) fails to restore with NU1605.

### Moving to 7.2 (Uno Platform 7)

- The packages are built against Uno Platform 7 and require .NET 10. Uno Platform 7 renders every target with Skia, so a single `net10.0` build serves the desktop, WebAssembly, Android and iOS heads. Binaries built for Uno Platform 6 or earlier cannot be loaded by an Uno Platform 7 app.
- The XAML Behaviors dependency is now `Uno.Microsoft.Xaml.Behaviors.Interactivity.WinUI` 4.x, the only line built for Uno Platform 7. Since XAML Behaviors 3.0, the `Microsoft.Xaml.Interactions` assembly is gone: `EventTriggerBehavior`, `InvokeCommandAction`, `DataTriggerBehavior`, `ControlStoryboardAction` and the rest of `Microsoft.Xaml.Interactions.Core` and `Microsoft.Xaml.Interactions.Media` live in `Microsoft.Xaml.Interactivity`. Point the existing prefixes at it, e.g. `xmlns:core="using:Microsoft.Xaml.Interactivity"`, and the elements need no change. A Windows head that shares the XAML needs `Microsoft.Xaml.Behaviors.WinUI.Managed` 3.x, because Microsoft's toolkit 7.1.2 brings 2.0.8.
- `DataGrid` honors `[ReadOnly]` on auto-generated columns, as it does on Windows.
- `UserInteractionModeStateTrigger` never activates on Uno Platform, because `UIViewSettings.GetForCurrentView()` is not implemented there. It used to throw and fail the page that used it.
- `ScrollViewerExtensions.EnableMiddleClickScrolling` does nothing on Uno Platform 7, because it tracks the pointer through `CoreWindow`, which is `null` for Uno Platform 7 windows. It used to throw.
- `CommunityToolkit.WinUI.Notifications` is not part of the Uno Platform build: it targets Windows only and was never published as an Uno package.
- Windows (WinAppSDK) heads keep using the packages published by Microsoft, as before.

The following packages are available:

## [WinUI / WinAppSDK]

- Uno.CommunityToolkit.Common [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.Common.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.Common)
- Uno.CommunityToolkit.WinUI [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI)
- Uno.CommunityToolkit.WinUI.Connectivity [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.Connectivity.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.Connectivity)
- Uno.CommunityToolkit.WinUI.DeveloperTools [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.DeveloperTools.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.DeveloperTools)
- Uno.CommunityToolkit.WinUI.UI [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI)
- Uno.CommunityToolkit.WinUI.UI.Animations [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Animations.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Animations)
- Uno.CommunityToolkit.WinUI.UI.Behaviors [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Behaviors.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Behaviors)
- Uno.CommunityToolkit.WinUI.UI.Controls [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls)
- Uno.CommunityToolkit.WinUI.UI.Controls.Core [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.Core.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.Core)
- Uno.CommunityToolkit.WinUI.UI.Controls.DataGrid [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.DataGrid.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.DataGrid)
- Uno.CommunityToolkit.WinUI.UI.Controls.Input [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.Input.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.Input)
- Uno.CommunityToolkit.WinUI.UI.Controls.Layout [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.Layout.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.Layout)
- Uno.CommunityToolkit.WinUI.UI.Controls.Markdown [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.Markdown.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.Markdown)
- Uno.CommunityToolkit.WinUI.UI.Controls.Media [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.Media.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.Media)
- Uno.CommunityToolkit.WinUI.UI.Controls.Primitives [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Controls.Primitives.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Controls.Primitives)
- Uno.CommunityToolkit.WinUI.UI.Media [![NuGet](https://img.shields.io/nuget/v/Uno.CommunityToolkit.WinUI.UI.Media.svg)](https://www.nuget.org/packages/Uno.CommunityToolkit.WinUI.UI.Media)

These package IDs are for Uno Platform (non-Windows) projects. For WinUI 3 projects, you should use the equivalent packages published by Microsoft (`CommunityToolkit.WinUI`, `CommunityToolkit.WinUI.UI.Controls` etc).

## Using the Uno Platform Windows Community Toolkit packages

For how to use Windows Community Toolkit, complete details are available [here](https://aka.platform.uno/install-uno-community-toolkit).


