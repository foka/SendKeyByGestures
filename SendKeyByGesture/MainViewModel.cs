using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;
using Microsoft.Kinect.Toolkit;
using Microsoft.Samples.Kinect.WpfViewers;
using System.Linq;
using SendKeyByGesture.Gestures;
using Timer = System.Timers.Timer;

namespace SendKeyByGesture
{
	public class MainViewModel : INotifyPropertyChanged
	{
		public MainViewModel(KinectSensorChooser kinectSensorChooser,
			PlayerPreviewViewModel playerPreviewViewModel,
			GestureWithKeyViewModel[] gestureWithKeyCollection)
		{
			KinectSensorChooser = kinectSensorChooser;
			PlayerPreviewViewModel = playerPreviewViewModel;
			KinectSensorManager = new KinectSensorManager();
			gestureControllers = new Dictionary<int, GestureController>();

			GestureWithKeyCollection = gestureWithKeyCollection;
			gesturesDictionary = GestureWithKeyCollection.ToDictionary(g => g.GestureName, g => g);
			gestureControllersLock = new object();

			processGestures = true;
			const double milisecondsBetweenGesturesMinimum = 500;
			frequentGestureTimer = new Timer(milisecondsBetweenGesturesMinimum);
			frequentGestureTimer.Elapsed += (_, __) => { lock (frequentGestureLock) processGestures = true; };
			frequentGestureLock = new object();
		}


		public KinectSensorChooser KinectSensorChooser { get; private set; }
		public KinectSensorManager KinectSensorManager { get; private set; }
		public PlayerPreviewViewModel PlayerPreviewViewModel { get; private set; }

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
			KinectSensorManager.SkeletonEnableTrackingInNearMode = false;
			KinectSensorManager.TransformSmoothParameters = new TransformSmoothParameters
			{
				Smoothing = 0.99f,
				Correction = 0.1f,
				Prediction = 0.1f,
				JitterRadius = 0.05f,
				MaxDeviationRadius = 0.05f,
			};
			KinectSensorManager.DepthStreamEnabled = true;
			KinectSensorManager.ColorStreamEnabled = true;
		}

		private void StopKinect(KinectSensor sensor)
		{
			sensor.Stop();
		}

		private void OnGestureRecognized(object sender, GestureEventArgs e)
		{
			lock(frequentGestureLock)
			{
				if (!processGestures) return;

				processGestures = false;
				frequentGestureTimer.Stop();
				frequentGestureTimer.Start();
			}

			RaiseGestureRecognized(e);
			SendKeys.SendWait(gesturesDictionary[e.GestureName].Keys);

			Log = string.Format("[{0:HH:mm:ss.fff}] {1} \n", DateTime.Now, e.GestureName)
			      + Log;
		}


		private void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
		{
			using (SkeletonFrame frame = e.OpenSkeletonFrame())
			{
				if (frame == null)
					return;

				if (skeletons.Length != frame.SkeletonArrayLength)
					skeletons = new Skeleton[frame.SkeletonArrayLength];

				frame.CopySkeletonDataTo(skeletons);

				foreach (var skeleton in skeletons)
				{
					if (skeleton.TrackingState != SkeletonTrackingState.Tracked)
						continue;

					var gestureController = GetGestureController(skeleton.TrackingId);

					gestureController.UpdateAllGestures(skeleton);
				}
			}
		}

		private GestureController GetGestureController(int trackingId)
		{
			lock (gestureControllersLock)
			{
				if (gestureControllers.ContainsKey(trackingId))
					return gestureControllers[trackingId];

				var gestureController = new GestureController();
				gestureController.GestureRecognized += OnGestureRecognized;
				foreach (var g in GesturesRegistry.Gestures)
				{
					gestureController.AddGesture(g.Key, g.Value);
				}
				return gestureControllers[trackingId] = gestureController;
			}
		}

		public event EventHandler<GestureEventArgs> GestureRecognized;

		public void RaiseGestureRecognized(GestureEventArgs e)
		{
			var handler = GestureRecognized;
			if (handler != null) handler(this, e);
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}


		private string log;
		private Skeleton[] skeletons = new Skeleton[0];
		private GestureWithKeyViewModel[] gestureWithKeyCollection;
		private readonly IDictionary<string, GestureWithKeyViewModel> gesturesDictionary;
		// key: trackingId
		private readonly IDictionary<int, GestureController> gestureControllers;
		private readonly object gestureControllersLock;

		private readonly object frequentGestureLock;
		private readonly Timer frequentGestureTimer;
		private bool processGestures;
	}
}