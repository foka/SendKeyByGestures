using System;
using System.Collections.Generic;
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
		public App()
		{
			gestureImageUri = new Dictionary<string, string>
			{
			    { GesturesRegistry.HR_Kozakiewicz, "/Images/Kozakiewicz.jpg" }
			};
		}


		private void App_OnStartup(object sender, StartupEventArgs e)
		{
			var kinectSensorChooser = new KinectSensorChooser();

			var playerPreviewViewModel = new PlayerPreviewViewModel(kinectSensorChooser);
			var mainWindowViewModel = new MainWindowViewModel(kinectSensorChooser, playerPreviewViewModel);
			mainWindow = new MainWindow(mainWindowViewModel);

			playerWindow = new PlayerPreviewWindow (playerPreviewViewModel)
			{
				Topmost = true,
				WindowStyle = WindowStyle.None,
				AllowsTransparency = true,
				ShowInTaskbar = false,
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
				var imageSource = GetImageForGesture(args.GestureName);
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

		private ImageSource GetImageForGesture(string gesture)
		{
			return gestureImageUri.ContainsKey(gesture)
				? new BitmapImage(new Uri("pack://application:,,," + gestureImageUri[gesture]))
				: null;
		}

		private ImageWindow imageWindow;
		private MainWindow mainWindow;
		private PlayerPreviewWindow playerWindow;
		private readonly IDictionary<string, string> gestureImageUri;
	}
}
