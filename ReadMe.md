# Uno Platform port of Windows Community Toolkit

This port allows for [Uno Platform based](https://github.com/unoplatform/uno) apps to use [Windows Community Toolkit](https://github.com/Microsoft/WindowsCommunityToolkit) for WinUI 3
on Windows, iOS, macOS, Android, WebAssembly and Linux.

See below on this page for information about UWP.

The following packages are available:
- Uno.CommunityToolkit.Common [![NuGet](https://img.shields.io/nuget/v/CommunityToolkit.Common.svg)](https://www.nuget.org/packages/CommunityToolkit.Common)
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

On Windows projects (the WinUI 3 Desktop head), please install the official [Windows Community Toolkit packages](https://github.com/Microsoft/WindowsCommunityToolkit) for WinUI 3.

If you are building a library, use the following to conditionally include the toolkit builds:

```xml
<ItemGroup Condition="$(TargetFramework.Contains('windows10'))">
	<PackageReference Include="CommunityToolkit.WinUI.Controls" Version="7.1.2" />
</ItemGroup>
<ItemGroup Condition="!$(TargetFramework.Contains('windows10'))">
	<PackageReference Include="Uno.CommunityToolkit.WinUI.Controls" Version="7.1.200" />
</ItemGroup>
```

For example, using the default Uno template, you only need to add the following lines (as needed) to the class library `<AppName>.csproj`. No changes are needed in each target separately (`<AppName>.Wasm.csproj`, `<AppName>.Windows.csproj`, etc.)
```xml
<ItemGroup Condition="$(TargetFramework.Contains('windows10'))">
  <PackageReference Include="CommunityToolkit.Common" />
  <PackageReference Include="CommunityToolkit.WinUI" />
  <PackageReference Include="CommunityToolkit.WinUI.Connectivity" />
  <PackageReference Include="CommunityToolkit.WinUI.DeveloperTools" />
  <PackageReference Include="CommunityToolkit.WinUI.UI" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Animations" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Behaviors" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.Core" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.DataGrid" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.Input" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.Layout" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.Markdown" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.Media" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Controls.Primitives" />
  <PackageReference Include="CommunityToolkit.WinUI.UI.Media" />
</ItemGroup>
<ItemGroup Condition="!$(TargetFramework.Contains('windows10'))">
  <PackageReference Include="Uno.CommunityToolkit.Common" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.Connectivity" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.DeveloperTools" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Animations" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Behaviors" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.Core" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.DataGrid" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.Input" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.Layout" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.Markdown" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.Media" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Controls.Primitives" />
  <PackageReference Include="Uno.CommunityToolkit.WinUI.UI.Media" />
</ItemGroup>
```

For the class library 

## Support for UWP

As the original Community Toolkit does, this fork also provides binaries for UWP, and the branch [`unorel/7.1`](https://github.com/unoplatform/Uno.WindowsCommunityToolkit/tree/unorel/7.1) is used to provide this support.

The following packages are available from this branch:
- Uno.Microsoft.Toolkit [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit)
- Uno.Microsoft.Toolkit.Uwp [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp)
- Uno.Microsoft.Toolkit.Uwp.Connectivity [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.Connectivity.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.Connectivity)
- Uno.Microsoft.Toolkit.Uwp.DeveloperTools [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.DeveloperTools.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.DeveloperTools)
- Uno.Microsoft.Toolkit.Uwp.UI [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI)
- Uno.Microsoft.Toolkit.Uwp.UI.Animations [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Animations.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Animations)
- Uno.Microsoft.Toolkit.Uwp.UI.Behaviors [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Behaviors.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Behaviors)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.Core [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Core.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Core)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.DataGrid [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.DataGrid.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.DataGrid)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.Input [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Input.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Input)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.Layout [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Layout.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Layout)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.Markdown [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Markdown.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Markdown)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.Media [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Media.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Media)
- Uno.Microsoft.Toolkit.Uwp.UI.Controls.Primitives [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Primitives.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Controls.Primitives)
- Uno.Microsoft.Toolkit.Uwp.UI.Media [![NuGet](https://img.shields.io/nuget/v/Uno.Microsoft.Toolkit.Uwp.UI.Media.svg)](https://www.nuget.org/packages/Uno.Microsoft.Toolkit.Uwp.UI.Media)

## Using the Uno Platform Windows Community Toolkit packages for UWP

These packages are providing support for the Uno Platform supported targets (iOS, Android, macOS, WebAssembly and Skia GTK/WPF/Tizen). 

On Windows projects (the UWP head), please install the official [Windows Community Toolkit packages](https://github.com/Microsoft/WindowsCommunityToolkit).

If you are building a library, use the following to conditionally include the toolkit builds:

```xml
<ItemGroup Condition="'$(TargetFramework)' == 'uap10.0.17763'">
	<PackageReference Include="Microsoft.Toolkit.Uwp.Controls" Version="7.1.10" />
</ItemGroup>
<ItemGroup Condition="'$(TargetFramework)' != 'uap10.0.17763'">
	<PackageReference Include="Uno.Microsoft.Toolkit.Uwp.Controls" Version="7.1.10" />
</ItemGroup>
```
