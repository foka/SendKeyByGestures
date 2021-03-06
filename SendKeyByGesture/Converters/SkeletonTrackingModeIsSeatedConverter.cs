using System;
using System.Globalization;
using System.Windows.Data;
using Microsoft.Kinect;

namespace SendKeyByGesture.Converters
{
	public class SkeletonTrackingModeIsSeatedConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is SkeletonTrackingMode && ((SkeletonTrackingMode)value) == SkeletonTrackingMode.Seated;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value is bool && (bool) value ? SkeletonTrackingMode.Seated : SkeletonTrackingMode.Default;
		}
	}
}