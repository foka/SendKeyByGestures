using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
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
			var gesturesWithKeys = GesturesRegistry.CreateGesturesWithKeys();

			var playerPreviewViewModel = new PlayerPreviewViewModel(kinectSensorChooser);
			var mainWindowViewModel = new MainWindowViewModel(kinectSensorChooser, playerPreviewViewModel, gesturesWithKeys);
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

			mainWindowViewModel.GestureRecognized += (_, args) =>
			{
				var imageSource = GetImageForGesture(args.GestureName);
				if (imageSource == null)
					return;

				imageWindow.ShowImage(imageSource);
			};

			mainWindow.Closed += (_, __) =>
			{
				SaveConfig(gesturesWithKeys, playerPreviewViewModel);
				imageWindow.Close();
				playerWindow.Close();
			};
			mainWindow.Loaded += (_, __) => SetWindowsOnMainWindowScreen();
			mainWindow.LocationChanged += (_, __) => SetWindowsOnMainWindowScreen();

			LoadConfig(gesturesWithKeys, playerPreviewViewModel);

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


		private void LoadConfig(IEnumerable<GestureWithKeyViewModel> gesturesWithKeys,
			PlayerPreviewViewModel playerPreviewViewModel)
		{
			var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
			foreach (var g in gesturesWithKeys)
			{
				g.Keys = GetSetting<string>(config, "Gesture_" + g.GestureName);
			}

			playerPreviewViewModel.Opacity = GetSetting<decimal>(config, "UserOpacity");
			playerPreviewViewModel.Brightness = GetSetting<int>(config, "UserBrightness");
		}

		private void SaveConfig(IEnumerable<GestureWithKeyViewModel> gesturesWithKeys,
			PlayerPreviewViewModel playerPreviewViewModel)
		{
			var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
			foreach (var g in gesturesWithKeys)
			{
				SetSetting(config, "Gesture_" + g.GestureName, g.Keys);
			}

			SetSetting(config, "UserOpacity", playerPreviewViewModel.Opacity);
			SetSetting(config, "UserBrightness", playerPreviewViewModel.Brightness);

			config.Save();
		}

		private T GetSetting<T>(Configuration config, string setting)
		{
			var appSetting = config.AppSettings.Settings[setting];
			if (appSetting == null)
				return default(T);
			return (T)Convert.ChangeType(appSetting.Value, typeof(T));
		}

		private void SetSetting(Configuration config, string setting, object value)
		{
			var appSetting = config.AppSettings.Settings[setting];
			if (appSetting == null)
			{
				config.AppSettings.Settings.Add(setting, value.ToString());
			}
			else
			{
				appSetting.Value = value.ToString();
			}
		}


		private ImageWindow imageWindow;
		private MainWindow mainWindow;
		private PlayerPreviewWindow playerWindow;
		private readonly IDictionary<string, string> gestureImageUri;
	}
}
