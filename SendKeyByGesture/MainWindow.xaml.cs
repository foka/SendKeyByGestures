using System.Windows;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Samples.Kinect.WpfViewers;

namespace SendKeyByGesture
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow
	{
		public MainWindow()
		{
			KinectSensorChooser = new KinectSensorChooser();
			KinectSensorManager = new KinectSensorManager();

			InitializeComponent();
		}


		public KinectSensorChooser KinectSensorChooser { get; private set; }
		public KinectSensorManager KinectSensorManager { get; private set; }


		private void StartKinect(KinectSensor sensor)
		{
			sensor.Start();

			KinectSensorManager.ElevationAngle = sensor.ElevationAngle;
			KinectSensorManager.KinectSensor = sensor;
			KinectSensorManager.SkeletonStreamEnabled = true;
//			KinectSensorManager.TransformSmoothParameters = new TransformSmoothParameters
//			{
//				Smoothing = 0.99f,
//				Correction = 0.1f,
//				Prediction = 0.1f,
//				JitterRadius = 0.05f,
//				MaxDeviationRadius = 0.05f,
//			};
			KinectSensorManager.SkeletonTrackingMode = SkeletonTrackingMode.Seated;
			KinectSensorManager.SkeletonEnableTrackingInNearMode = true;
//			KinectSensorManager.DepthStreamEnabled = true;
			KinectSensorManager.ColorStreamEnabled = true;
		}

		private void StopKinect(KinectSensor sensor)
		{
			sensor.Stop();
		}


		private void KinectChanged(object sender, KinectChangedEventArgs e)
		{
			if (e.OldSensor != null)
				StopKinect(e.OldSensor);
			if (e.NewSensor != null)
				StartKinect(e.NewSensor);
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			kinectSensorChooserUI.KinectSensorChooser = KinectSensorChooser;
			KinectSensorChooser.KinectChanged += KinectChanged;
			KinectSensorChooser.Start();
		}

		private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (KinectSensorChooser.Kinect != null)
				StopKinect(KinectSensorChooser.Kinect);
		}
	}
}
