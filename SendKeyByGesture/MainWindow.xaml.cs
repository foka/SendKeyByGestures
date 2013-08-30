using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		private readonly MainWindowViewModel viewModel;

		public MainWindow(MainWindowViewModel mainWindowViewModel)
		{
			viewModel = mainWindowViewModel;
			DataContext = mainWindowViewModel;
			InitializeComponent();
		}


		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			kinectSensorChooserUI.KinectSensorChooser = viewModel.KinectSensorChooser;
			viewModel.Start();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			viewModel.Close();
		}

		private void Hyperlink_OnClick(object sender, RoutedEventArgs e)
		{
			Process.Start("http://msdn.microsoft.com/en-us/library/system.windows.forms.sendkeys.aspx");
		}


		private void tiltSlider_OnPreviewMouseUp(object sender, MouseButtonEventArgs e)
		{
			if (tiltSlider.IsFocused)
				viewModel.KinectSensorManager.ElevationAngle = (int) Math.Round(tiltSlider.Value);
		}

		private void tiltSlider_OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
		{
			if (tiltSlider.IsFocused && Mouse.LeftButton != MouseButtonState.Pressed)
				viewModel.KinectSensorManager.ElevationAngle = (int)Math.Round(tiltSlider.Value);
		}

		private void CameraCheckBox_OnChecked(object sender, RoutedEventArgs e)
		{
			colorViewer.Visibility = Visibility.Visible;
			depthViewer.Visibility = Visibility.Hidden;
		}

		private void CameraCheckBox_OnUnchecked(object sender, RoutedEventArgs e)
		{
			colorViewer.Visibility = Visibility.Hidden;
			depthViewer.Visibility = Visibility.Visible;
		}
	}
}
