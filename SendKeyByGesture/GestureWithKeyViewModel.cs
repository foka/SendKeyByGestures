using System.ComponentModel;

namespace SendKeyByGesture
{
	public class GestureWithKeyViewModel : INotifyPropertyChanged
	{
		public virtual string GestureName
		{
			get { return gestureName; }
			set
			{
				if (value == gestureName) return;
				gestureName = value;
				RaisePropertyChanged(GesstureProperty);
			}
		}

		public virtual string Key
		{
			get { return key; }
			set
			{
				if (value == key) return;
				key = value;
				RaisePropertyChanged(KeyProperty);
			}
		}

		public const string KeyProperty = "Key";
		private string key;

		public const string GesstureProperty = "Gessture";
		private string gestureName;

		public event PropertyChangedEventHandler PropertyChanged;

		private void RaisePropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}