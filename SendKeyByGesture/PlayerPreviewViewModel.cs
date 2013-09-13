using System;
using System.ComponentModel;
using Microsoft.Kinect.Toolkit;
using Microsoft.Samples.Kinect.WpfViewers;

namespace SendKeyByGesture
{
	public class PlayerPreviewViewModel : INotifyPropertyChanged
	{
		private const int BrightnessMin = 0;
		private const int BrightnessMax = 100;
		private const decimal OpacityMin = 0m;
		private const decimal OpacityMax = 1m;


		public PlayerPreviewViewModel(KinectSensorChooser kinectSensorChooser, KinectSensorManager kinectSensorManager)
		{
			KinectSensorChooser = kinectSensorChooser;
			KinectSensorManager = kinectSensorManager;
		}


		public KinectSensorChooser KinectSensorChooser { get; private set; }
		public KinectSensorManager KinectSensorManager { get; private set; }

		/// <summary>
		/// Gets the user opacity (0.0 = transparent, 1.0 = opaque).
		/// </summary>
		public decimal Opacity
		{
			get { return opacity; }
			set
			{
				if (value == opacity) return;
				
				value = Math.Max(OpacityMin, Math.Min(OpacityMax, value));
				opacity = value;
				RaisePropertyChanged(OpacityProperty);
			}
		}
		public static readonly string OpacityProperty = "Opacity";
		private decimal opacity;

		/// <summary>
		/// Gets or sets the user brightness (0 = black, 100 = white).
		/// </summary>
		public int Brightness
		{
			get { return brightness; }
			set
			{
				if (value == brightness) return;

				value = Math.Max(BrightnessMin, Math.Min(BrightnessMax, value));
				brightness = value;
				RaisePropertyChanged(BrightnessProperty);
			}
		}

		public static readonly string BrightnessProperty = "Brightness";
		private int brightness;


		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}