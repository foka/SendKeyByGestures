using System.ComponentModel;

namespace SendKeyByGesture
{
	public class GestureWithKeyViewModel : INotifyPropertyChanged
	{
		public static readonly string GesstureProperty = "Gessture";
		public static readonly string KeyProperty = "Key";

		public string GestureName
		{
			get { return gestureName; }
			set
			{
				if (value == gestureName) return;
				gestureName = value;
				RaisePropertyChanged(GesstureProperty);
			}
		}

		public string Key
		{
			get { return key; }
			set
			{
				if (value == key) return;
				key = value;
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
		private string key;
	}
}