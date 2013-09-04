using System.Collections.Generic;
using System.Linq;
using Fizbin.Kinect.Gestures;
using Fizbin.Kinect.Gestures.Segments;
using SendKeyByGesture.Gestures.Segments;

namespace SendKeyByGesture.Gestures
{
	public static class GesturesRegistry
	{
		public static readonly IDictionary<string, IRelativeGestureSegment[]> Gestures = new Dictionary<string, IRelativeGestureSegment[]>();

		static GesturesRegistry()
		{
			CreateGestures();
		}

		public static GestureWithKeyViewModel[] CreateGesturesWithKeys()
		{
			return Gestures.Select(g => new GestureWithKeyViewModel { GestureName = g.Key }).ToArray();
		}

		private static void CreateGestures()
		{
			var joinedhandsSegments = new IRelativeGestureSegment[20];
			var joinedhandsSegment = new JoinedHandsSegment1();
			for (int i = 0; i < 20; i++)
			{
				// gesture consists of the same thing 10 times 
				joinedhandsSegments[i] = joinedhandsSegment;
			}
			Gestures.Add("JoinedHands", joinedhandsSegments);

			var waveRightSegments = new IRelativeGestureSegment[4];
			var waveRightSegment1 = new WaveRightSegment1();
			var waveRightSegment2 = new WaveRightSegment2();
			waveRightSegments[0] = waveRightSegment1;
			waveRightSegments[1] = waveRightSegment2;
			waveRightSegments[2] = waveRightSegment1;
			waveRightSegments[3] = waveRightSegment2;
			Gestures.Add("WaveRight", waveRightSegments);

			var waveLeftSegments = new IRelativeGestureSegment[4];
			var waveLeftSegment1 = new WaveLeftSegment1();
			var waveLeftSegment2 = new WaveLeftSegment2();
			waveLeftSegments[0] = waveLeftSegment1;
			waveLeftSegments[1] = waveLeftSegment2;
			waveLeftSegments[2] = waveLeftSegment1;
			waveLeftSegments[3] = waveLeftSegment2;
			Gestures.Add("WaveLeft", waveLeftSegments);

			var rightHandSwipeLeft = new IRelativeGestureSegment[3];
			rightHandSwipeLeft[0] = new RightHandSwipeLeftSegment1();
			rightHandSwipeLeft[1] = new RightHandSwipeLeftSegment2();
			rightHandSwipeLeft[2] = new RightHandSwipeLeftSegment3();
			Gestures.Add("RightHandSwipeLeft", rightHandSwipeLeft);

			var rightHandSwipeRight = new IRelativeGestureSegment[3];
			rightHandSwipeRight[0] = new RightHandSwipeLeftSegment3();
			rightHandSwipeRight[1] = new RightHandSwipeLeftSegment2();
			rightHandSwipeRight[2] = new RightHandSwipeLeftSegment1();
			Gestures.Add("RightHandSwipeRight", rightHandSwipeRight);

			var leftHandSwipeRight = new IRelativeGestureSegment[3];
			leftHandSwipeRight[0] = new LeftHandSwipeRightSegment1();
			leftHandSwipeRight[1] = new LeftHandSwipeRightSegment2();
			leftHandSwipeRight[2] = new LeftHandSwipeRightSegment3();
			Gestures.Add("LeftHandSwipeRight", leftHandSwipeRight);

			var leftHandSwipeLeft = new IRelativeGestureSegment[3];
			leftHandSwipeLeft[0] = new LeftHandSwipeRightSegment3();
			leftHandSwipeLeft[1] = new LeftHandSwipeRightSegment2();
			leftHandSwipeLeft[2] = new LeftHandSwipeRightSegment1();
			Gestures.Add("LeftHandSwipeLeft", leftHandSwipeLeft);

			var rightKozakiewicz = new IRelativeGestureSegment[]
			{
				new RightKozakiewiczSegment1(),
				new RightKozakiewiczSegment2(),
				new RightKozakiewiczSegment3(),
				new RightKozakiewiczSegment4(),
			};
			Gestures.Add("RightKozakiewicz", rightKozakiewicz);
		}

		public static readonly string RightHandSwipeLeft = "RightHandSwipeLeft";
		public static readonly string RightHandSwipeRight = "RightHandSwipeRight";
		public static readonly string LeftHandSwipeRight = "LeftHandSwipeRight";
		public static readonly string LeftHandSwipeLeft = "LeftHandSwipeLeft";
	}
}