using AppKit;

namespace Microsoft.Toolkit.Uwp.SampleApp.macOS
{
    static class MainClass
	{
		static void Main(string[] args)
		{
			NSApplication.Init();
			NSApplication.SharedApplication.Delegate = new App();
			NSApplication.Main(args);  
		}
	}
}

