using System;
using System.Diagnostics;
using System.Windows;
using Fizbin.Kinect.Gestures;
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
		private Skeleton[] skeletons = new Skeleton[0];
		private GestureController gestureController;


		private void StartKinect(KinectSensor sensor)
		{
			sensor.Start();
			sensor.SkeletonFrameReady += sensor_SkeletonFrameReady;

			KinectSensorManager.ElevationAngle = sensor.ElevationAngle;
			KinectSensorManager.KinectSensor = sensor;
			KinectSensorManager.SkeletonStreamEnabled = true;
			KinectSensorManager.TransformSmoothParameters = new TransformSmoothParameters
			{
				Smoothing = 0.99f,
				Correction = 0.1f,
				Prediction = 0.1f,
				JitterRadius = 0.05f,
				MaxDeviationRadius = 0.05f,
			};
//			KinectSensorManager.SkeletonTrackingMode = SkeletonTrackingMode.Seated;
			KinectSensorManager.SkeletonEnableTrackingInNearMode = true;
			KinectSensorManager.DepthStreamEnabled = true;
			KinectSensorManager.ColorStreamEnabled = true;

			gestureController = new GestureController();
			gestureController.GestureRecognized += OnGestureRecognized;
			GesturesRegistar.RegisterGestures(gestureController);
		}

		private void OnGestureRecognized(object sender, GestureEventArgs e)
		{
			gestureLogTextBox.Text =  DateTime.Now.ToString("[HH:mm:ss.fff]  ") + e.GestureName + "\n" + gestureLogTextBox.Text;
		}

		private void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
		{
			using (SkeletonFrame frame = e.OpenSkeletonFrame())
			{
				if (frame == null)
					return;

				// resize the skeletons array if needed
				if (skeletons.Length != frame.SkeletonArrayLength)
					skeletons = new Skeleton[frame.SkeletonArrayLength];

				// get the skeleton data
				frame.CopySkeletonDataTo(skeletons);

				foreach (var skeleton in skeletons)
				{
					// skip the skeleton if it is not being tracked
					if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
						continue;

					// update the gesture controller
					gestureController.UpdateAllGestures(skeleton);
				}
			}
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
