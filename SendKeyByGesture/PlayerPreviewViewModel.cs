using System;
using System.ComponentModel;
using Microsoft.Kinect.Toolkit;

namespace SendKeyByGesture
{
	public class PlayerPreviewViewModel : INotifyPropertyChanged
	{
		private const int brightnessMin = 0;
		private const int brightnessMax = 100;


		public PlayerPreviewViewModel(KinectSensorChooser kinectSensorChooser)
		{
			KinectSensorChooser = kinectSensorChooser;
			UserBrightness = 75;
		}


		public KinectSensorChooser KinectSensorChooser { get; set; }

//		public Color DefaultUserColor
//		{
//			get { return defaultUserColor; }
//			private set
//			{
//				if (value == defaultUserColor) return;
//				defaultUserColor = value;
//				RaisePropertyChanged(DefaultUserColorProperty);
//			}
//		}
//		public static readonly string DefaultUserColorProperty = "DefaultUserColor";
//		private Color defaultUserColor;

		/// <summary>
		/// Gets or sets the user brightness (0 = black, 100 = white).
		/// </summary>
		public int UserBrightness
		{
			get { return userBrightness; }
			set
			{
				if (value == userBrightness) return;

				value = Math.Max(brightnessMin, Math.Min(brightnessMax, value));
				userBrightness = value;
				RaisePropertyChanged(UserBrightnessProperty);
			}
		}

		public static readonly string UserBrightnessProperty = "UserBrightness";
		private int userBrightness;





		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}