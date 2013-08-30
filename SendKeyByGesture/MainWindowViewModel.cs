using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Windows.Forms;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Samples.Kinect.WpfViewers;
using System.Linq;

namespace SendKeyByGesture
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		public MainWindowViewModel()
		{
			KinectSensorChooser = new KinectSensorChooser();
			KinectSensorManager = new KinectSensorManager();

			GestureWithKeyCollection = GesturesRegistry.CreateGesturesWithKeys();
			LoadConfig();
			gesturesDictionary = GestureWithKeyCollection.ToDictionary(g => g.GestureName, g => g);
		}


		public KinectSensorChooser KinectSensorChooser { get; private set; }
		public KinectSensorManager KinectSensorManager { get; private set; }

		public string Log
		{
			get { return log; }
			private set
			{
				if (value == log) return;
				log = value;
				RaisePropertyChanged("Log");
			}
		}

		public GestureWithKeyViewModel[] GestureWithKeyCollection
		{
			get { return gestureWithKeyCollection; }
			private set
			{
				gestureWithKeyCollection = value;
				RaisePropertyChanged("GestureWithKeyCollection");
			}
		}


		public void Start()
		{
			KinectSensorChooser.KinectChanged += KinectChanged;
			KinectSensorChooser.Start();
		}

		public void Close()
		{
			if (KinectSensorChooser.Kinect != null)
				StopKinect(KinectSensorChooser.Kinect);
			SaveConfig();
		}


		private void LoadConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
			foreach (var g in GestureWithKeyCollection)
			{
				var appSetting = config.AppSettings.Settings["Gesture_" + g.GestureName];
				g.Keys = appSetting == null ? null : appSetting.Value;
			}
		}

		private void SaveConfig()
		{
			var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
			foreach (var g in GestureWithKeyCollection)
			{
				var appSettingKey = "Gesture_" + g.GestureName;
				var appSetting = config.AppSettings.Settings[appSettingKey];
				if (appSetting == null)
					config.AppSettings.Settings.Add(appSettingKey, g.Keys);
				else
				{
					appSetting.Value = g.Keys;
				}
			}
			config.Save();
		}

		private void KinectChanged(object sender, KinectChangedEventArgs e)
		{
			if (e.OldSensor != null)
				StopKinect(e.OldSensor);
			if (e.NewSensor != null)
				StartKinect(e.NewSensor);
		}


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
			KinectSensorManager.SkeletonEnableTrackingInNearMode = true;
			KinectSensorManager.DepthStreamEnabled = true;
			KinectSensorManager.ColorStreamEnabled = true;

			gestureController = new GestureController();
			gestureController.GestureRecognized += OnGestureRecognized;
			foreach (var g in GesturesRegistry.Gestures)
			{
				gestureController.AddGesture(g.Key, g.Value);
			}
		}

		private void StopKinect(KinectSensor sensor)
		{
			sensor.Stop();
		}

		private void OnGestureRecognized(object sender, GestureEventArgs e)
		{
			Log = DateTime.Now.ToString("[HH:mm:ss.fff]  ") + e.GestureName + "\n" + Log;
			SendKeys.SendWait(gesturesDictionary[e.GestureName].Keys);
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


		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}


		private string log;
		private Skeleton[] skeletons = new Skeleton[0];
		private GestureController gestureController;
		private GestureWithKeyViewModel[] gestureWithKeyCollection;
		private readonly IDictionary<string, GestureWithKeyViewModel> gesturesDictionary;
	}
}