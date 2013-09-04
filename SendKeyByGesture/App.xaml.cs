using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Kinect.Toolkit;
using SendKeyByGesture.Gestures;
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

			var mainWindowViewModel = new MainWindowViewModel(kinectSensorChooser);
			mainWindow = new MainWindow(mainWindowViewModel);

			playerWindow = new PlayerPreviewWindow (new PlayerPreviewViewModel(kinectSensorChooser))
			{
				Topmost = true,
				WindowStyle = WindowStyle.None,
				AllowsTransparency = true,
				ShowInTaskbar = false,
				Opacity = 0.33
			};

			imageWindow = new ImageWindow
			{
				Topmost = true,
				WindowStyle = WindowStyle.None,
				AllowsTransparency = true,
			};


			mainWindow.Closed += (_, __) =>
			{
				imageWindow.Close();
				playerWindow.Close();
			};
			mainWindow.Loaded += (_, __) => SetWindowsOnMainWindowScreen();
			mainWindow.LocationChanged += (_, __) => SetWindowsOnMainWindowScreen();

			mainWindowViewModel.GestureRecognized += (_, args) => 
			{
				ImageSource imageSource = null;
				if (args.GestureName == GesturesRegistry.HR_Kozakiewicz)
				{
					imageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Kozakiewicz.jpg"));
				}
				if (imageSource == null)
					return;

				imageWindow.ShowImage(imageSource);
			};

			mainWindow.Show();
			playerWindow.Show();
		}

		private void SetWindowsOnMainWindowScreen()
		{
			if (mainWindow.WindowState == WindowState.Minimized) return;
			var isMinimizing = mainWindow.Left == -32000 && mainWindow.Top == -32000;
			if (isMinimizing) return;

			var mainWindowBounds = new System.Drawing.Rectangle(
				(int) mainWindow.Left, (int) mainWindow.Top, (int) mainWindow.Width, (int) mainWindow.Height);
			var screen = Screen.FromRectangle(mainWindowBounds);


			playerWindow.SetPosition(screen);
			imageWindow.SetPosition(screen);
		}

		private ImageWindow imageWindow;
		private MainWindow mainWindow;
		private PlayerPreviewWindow playerWindow;
	}
}
