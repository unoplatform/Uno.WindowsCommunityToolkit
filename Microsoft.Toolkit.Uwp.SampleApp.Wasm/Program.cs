using System;
using System.Threading;
using Microsoft.Extensions.Logging;
using Uno.Extensions;
using Windows.UI.Xaml;

namespace Microsoft.Toolkit.Uwp.SampleApp.Wasm
{
	public static class Program
	{
		private static App _app;

		static void Main(string[] args)
		{

			Application.Start(e => new App());
		}
	}
}
