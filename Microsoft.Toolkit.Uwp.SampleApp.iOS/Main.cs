using Microsoft.Extensions.Logging;
using UIKit;

namespace Microsoft.Toolkit.Uwp.SampleApp.iOS
{
	public class Application
	{
		// This is the main entry point of the application.
		static void Main(string[] args)
		{
			Uno.Extensions.LogExtensionPoint
			.AmbientLoggerFactory
			.AddConsole(LogLevel.Warning)
			.AddDebug(LogLevel.Warning);


			// if you want to use a different Application Delegate class from "AppDelegate"
			// you can specify it here.
			UIApplication.Main(args, null, typeof(App)); 
		}
	}
}  