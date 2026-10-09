// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Threading.Tasks;
using Uno.UI.Hosting;

namespace CommunityToolkit.WinUI.SampleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            App.InitializeLogging();

            var host = UnoPlatformHostBuilder.Create()
                .App(() => new App())
                .UseWebAssembly()
                .Build();

            await host.RunAsync();
        }
    }
}
