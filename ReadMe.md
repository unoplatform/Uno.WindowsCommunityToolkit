# Uno Platform port of Windows Community Toolkit

This port allows for [Uno Platform based](https://github.com/unoplatform/uno) apps to use [Windows Community Toolkit](https://github.com/Microsoft/WindowsCommunityToolkit)
on Windows, iOS, macOS, Android, WebAssembly and Linux.

The following packages are available:
- Uno.Microsoft.Toolkit [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit)
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

## Using the Uno Platform Windows Community Toolkit packages

These packages are providing support for the Uno Platform supported targets (iOS, Android, macOS, WebAssembly and Skia GTK/WPF/Tizen). 

On Windows projects (the UWP head), please install the official [Windows Community Toolkit packages](https://github.com/Microsoft/WindowsCommunityToolkit).

If you are building for library, use the following to conditionally include the toolkit builds:

```xml
<ItemGroup Condition="'$(TargetFramework)' == 'uap10.0.17763'">
	<PackageReference Include="CommunityToolkit.WinUI.Controls" Version="7.0.0" />
</ItemGroup>
<ItemGroup Condition="'$(TargetFramework)' != 'uap10.0.17763'">
	<PackageReference Include="Uno.CommunityToolkit.WinUI.Controls" Version="7.0.0" />
</ItemGroup>
```