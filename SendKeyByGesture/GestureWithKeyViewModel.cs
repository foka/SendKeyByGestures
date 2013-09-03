using System.ComponentModel;

namespace SendKeyByGesture
{
	public class GestureWithKeyViewModel : INotifyPropertyChanged
	{
		public static readonly string GestureProperty = "Gesture";
		public static readonly string KeyProperty = "Keys";

		public string GestureName
		{
			get { return gestureName; }
			set
			{
				if (value == gestureName) return;
				gestureName = value;
				RaisePropertyChanged(GestureProperty);
			}
		}

		public string Keys
		{
			get { return keys; }
			set
			{
				if (value == keys) return;
				keys = value;
				RaisePropertyChanged(KeyProperty);
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}

		private string gestureName;
		private string keys;
	}
}