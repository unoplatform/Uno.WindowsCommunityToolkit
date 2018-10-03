using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Uno.UI.WpfHost;

namespace Microsoft.Toolkit.Uwp.Samples.Wpf
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();

#if DEBUG
			var configuration = "Debug";
#else
			var configuration = "Release";
#endif

			UnoHostView.Init(() => Microsoft.Toolkit.Uwp.SampleApp.Wasm.Program.Main(new string[0]), $@"..\..\..\..\MyApp.Wasm\bin\{configuration}\netstandard2.0\dist");
		}
	}
}
