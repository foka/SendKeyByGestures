using Microsoft.Kinect.Toolkit;

namespace SendKeyByGesture
{
	public class PlayerPreviewViewModel
	{
		public KinectSensorChooser KinectSensorChooser { get; set; }

		public PlayerPreviewViewModel(KinectSensorChooser kinectSensorChooser)
		{
			KinectSensorChooser = kinectSensorChooser;
		}
	}
}