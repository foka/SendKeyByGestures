using System;
using System.Windows;
using Microsoft.Kinect.Toolkit;
using Application = System.Windows.Application;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var kinectSensorChooser = new KinectSensorChooser();

			var mainWindow = new MainWindow(new MainWindowViewModel(kinectSensorChooser));

			var playerWindow = new PlayerPreviewWindow (new PlayerPreviewViewModel(kinectSensorChooser))
			{
				Topmost = true,
				WindowStyle = WindowStyle.None,
				AllowsTransparency = true,
				ShowInTaskbar = false,
			};


			mainWindow.Closed += (_, __) => playerWindow.Close();
			mainWindow.Loaded += (_, __) => SetPlayerWindowPosition(playerWindow, mainWindow);
			mainWindow.LocationChanged += (_, __) => SetPlayerWindowPosition(playerWindow, mainWindow);


			mainWindow.Show();
			playerWindow.Show();
		}

		private static void SetPlayerWindowPosition(PlayerPreviewWindow playerWindow, MainWindow mainWindow)
		{
			if (mainWindow.WindowState == WindowState.Minimized) return;
			var isMinimizing = mainWindow.Left == -32000 && mainWindow.Top == -32000;
			if (isMinimizing) return;

			var mainWindowBounds = new System.Drawing.Rectangle(
				(int) mainWindow.Left, (int) mainWindow.Top, (int) mainWindow.Width, (int) mainWindow.Height);
			playerWindow.SetPosition(System.Windows.Forms.Screen.FromRectangle(mainWindowBounds));
		}
	}
}
