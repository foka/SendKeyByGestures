using System.Windows;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var window = new MainWindow(new MainWindowViewModel());
			window.Show();
		}
	}
}
