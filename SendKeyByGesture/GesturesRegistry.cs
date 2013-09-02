﻿using System.Collections.Generic;
using System.Linq;
using Fizbin.Kinect.Gestures;
using Fizbin.Kinect.Gestures.Segments;
using SendKeyByGesture.Segments;

namespace SendKeyByGesture
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
//			var joinedhandsSegments = new IRelativeGestureSegment[20];
//			var joinedhandsSegment = new JoinedHandsSegment1();
//			for (int i = 0; i < 20; i++)
//			{
//				// gesture consists of the same thing 10 times 
//				joinedhandsSegments[i] = joinedhandsSegment;
//			}
//			Gestures.Add("JoinedHands", joinedhandsSegments);
//
//			var menuSegments = new IRelativeGestureSegment[20];
//			var menuSegment = new MenuSegment1();
//			for (int i = 0; i < 20; i++)
//			{
//				// gesture consists of the same thing 20 times 
//				menuSegments[i] = menuSegment;
//			}
//			Gestures.Add("Menu", menuSegments);
//
//			var swipeleftSegments = new IRelativeGestureSegment[3];
//			swipeleftSegments[0] = new SwipeLeftSegment1();
//			swipeleftSegments[1] = new SwipeLeftSegment2();
//			swipeleftSegments[2] = new SwipeLeftSegment3();
//			Gestures.Add("SwipeLeft", swipeleftSegments);
//
//			var swiperightSegments = new IRelativeGestureSegment[3];
//			swiperightSegments[0] = new SwipeRightSegment1();
//			swiperightSegments[1] = new SwipeRightSegment2();
//			swiperightSegments[2] = new SwipeRightSegment3();
//			Gestures.Add("SwipeRight", swiperightSegments);
//
//			var waveRightSegments = new IRelativeGestureSegment[6];
//			var waveRightSegment1 = new WaveRightSegment1();
//			var waveRightSegment2 = new WaveRightSegment2();
//			waveRightSegments[0] = waveRightSegment1;
//			waveRightSegments[1] = waveRightSegment2;
//			waveRightSegments[2] = waveRightSegment1;
//			waveRightSegments[3] = waveRightSegment2;
//			waveRightSegments[4] = waveRightSegment1;
//			waveRightSegments[5] = waveRightSegment2;
//			Gestures.Add("WaveRight", waveRightSegments);
//
//			var waveLeftSegments = new IRelativeGestureSegment[6];
//			var waveLeftSegment1 = new WaveLeftSegment1();
//			var waveLeftSegment2 = new WaveLeftSegment2();
//			waveLeftSegments[0] = waveLeftSegment1;
//			waveLeftSegments[1] = waveLeftSegment2;
//			waveLeftSegments[2] = waveLeftSegment1;
//			waveLeftSegments[3] = waveLeftSegment2;
//			waveLeftSegments[4] = waveLeftSegment1;
//			waveLeftSegments[5] = waveLeftSegment2;
//			Gestures.Add("WaveLeft", waveLeftSegments);
//
//			var zoomInSegments = new IRelativeGestureSegment[3];
//			zoomInSegments[0] = new ZoomSegment1();
//			zoomInSegments[1] = new ZoomSegment2();
//			zoomInSegments[2] = new ZoomSegment3();
//			Gestures.Add("ZoomIn", zoomInSegments);
//
//			var zoomOutSegments = new IRelativeGestureSegment[3];
//			zoomOutSegments[0] = new ZoomSegment3();
//			zoomOutSegments[1] = new ZoomSegment2();
//			zoomOutSegments[2] = new ZoomSegment1();
//			Gestures.Add("ZoomOut", zoomOutSegments);
//
//			var swipeUpSegments = new IRelativeGestureSegment[3];
//			swipeUpSegments[0] = new SwipeUpSegment1();
//			swipeUpSegments[1] = new SwipeUpSegment2();
//			swipeUpSegments[2] = new SwipeUpSegment3();
//			Gestures.Add("SwipeUp", swipeUpSegments);
//
//			var swipeDownSegments = new IRelativeGestureSegment[3];
//			swipeDownSegments[0] = new SwipeDownSegment1();
//			swipeDownSegments[1] = new SwipeDownSegment2();
//			swipeDownSegments[2] = new SwipeDownSegment3();
//			Gestures.Add("SwipeDown", swipeDownSegments);

			var rightHandSwipeLeft = new IRelativeGestureSegment[3];
			rightHandSwipeLeft[0] = new RightHandSwipeLeftSegment1();
			rightHandSwipeLeft[1] = new RightHandSwipeLeftSegment2();
			rightHandSwipeLeft[2] = new RightHandSwipeLeftSegment3();
			Gestures.Add("RightHandSwipeLeft", rightHandSwipeLeft);

			var leftHandSwipeRight = new IRelativeGestureSegment[3];
			leftHandSwipeRight[0] = new LeftHandSwipeRightSegment1();
			leftHandSwipeRight[1] = new LeftHandSwipeRightSegment2();
			leftHandSwipeRight[2] = new LeftHandSwipeRightSegment3();
			Gestures.Add("LeftHandSwipeRight", leftHandSwipeRight);

		}
	}
}