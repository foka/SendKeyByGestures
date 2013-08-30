using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Kinect;

namespace SendKeyByGesture
{
	public class KinectSensorConnectedConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (value is KinectStatus) && ((KinectStatus)value) == KinectStatus.Connected;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}