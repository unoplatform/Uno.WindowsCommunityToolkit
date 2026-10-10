// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using Uno.UI.Hosting;

namespace CommunityToolkit.WinUI.RuntimeTests
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var host = UnoPlatformHostBuilder.Create()
                .App(() => new App())
                .UseX11()
                .UseLinuxFrameBuffer()
                .UseMacOS()
                .UseWin32()
                .Build();

            host.Run();
        }
    }
}
