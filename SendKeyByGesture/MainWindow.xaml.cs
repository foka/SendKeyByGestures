using System.Diagnostics;
using System.Windows;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow(MainWindowViewModel mainWindowViewModel)
		{
			DataContext = mainWindowViewModel;
			InitializeComponent();
		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			kinectSensorChooserUI.KinectSensorChooser = ((MainWindowViewModel)DataContext).KinectSensorChooser;
			((MainWindowViewModel)DataContext).Start();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			((MainWindowViewModel)DataContext).Close();
		}

		private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
		{
			Process.Start("http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx");
		}
	}
}
