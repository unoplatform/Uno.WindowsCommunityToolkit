using System;
using Uno.UI.Hosting;

namespace CommunityToolkit.WinUI.SampleApp
{
    public static class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            App.InitializeLogging();

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
