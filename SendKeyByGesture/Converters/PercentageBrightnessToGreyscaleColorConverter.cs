using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace SendKeyByGesture.Converters
{
	public class PercentageBrightnessToGreyscaleColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var brightness = System.Convert.ToInt32(value);
			var colorPart = (byte)Math.Round((brightness / 100d) * 255);
			return Color.FromRgb(colorPart, colorPart, colorPart);
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}