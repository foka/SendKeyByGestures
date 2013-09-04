using System;
using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace SendKeyByGesture.Gestures.Segments
{
	class KozakiewiczPositions
	{
		public static bool ElbowsBeneathShoulders(Skeleton skeleton)
		{
			var j = skeleton.Joints;

			return j[JointType.ElbowRight].Position.Y < j[JointType.ShoulderRight].Position.Y
				&& 0.2 >= Math.Abs(j[JointType.ElbowRight].Position.X - j[JointType.ShoulderRight].Position.X)
				&& j[JointType.ElbowLeft].Position.Y < j[JointType.ShoulderLeft].Position.Y
				&& 0.2 >= Math.Abs(j[JointType.ElbowLeft].Position.X - j[JointType.ShoulderLeft].Position.X);
		}

		public static bool LeftHandOnRightElbow(Skeleton skeleton)
		{
			var j = skeleton.Joints;

			return 0.2 >= Math.Abs(j[JointType.ElbowRight].Position.X - j[JointType.HandLeft].Position.X)
				&& 0.2 >= Math.Abs(j[JointType.ElbowRight].Position.Y - j[JointType.HandLeft].Position.Y);
		}
	}

	public class RightKozakiewiczSegment1 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			var j = skeleton.Joints;

			if (KozakiewiczPositions.ElbowsBeneathShoulders(skeleton))
			{
				// hands below elbows
				if (j[JointType.HandLeft].Position.Y < j[JointType.ElbowLeft].Position.Y
					&& j[JointType.HandRight].Position.Y < j[JointType.ElbowRight].Position.Y)
				{
					return GesturePartResult.Succeed;
				}
				return GesturePartResult.Fail;
			}
			return GesturePartResult.Fail;
		}
	}

	public class RightKozakiewiczSegment2 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			var j = skeleton.Joints;

			if (KozakiewiczPositions.ElbowsBeneathShoulders(skeleton))
			{
				// right hand still down
				// left hand "on" right elbow
				if (j[JointType.HandRight].Position.Y < j[JointType.ElbowRight].Position.Y
					&& KozakiewiczPositions.LeftHandOnRightElbow(skeleton))
				{
					return GesturePartResult.Succeed;
				}
				return GesturePartResult.Pausing;
			}
			return GesturePartResult.Fail;
		}
	}

	public class RightKozakiewiczSegment3 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			var j = skeleton.Joints;

			if (KozakiewiczPositions.ElbowsBeneathShoulders(skeleton))
			{
				// left hand still "on" right elbow
				// right hand half way going up
				if (KozakiewiczPositions.LeftHandOnRightElbow(skeleton)
					&& 0.3 >= Math.Abs(j[JointType.HandRight].Position.X - j[JointType.ElbowRight].Position.X)
					&& 0.3 >= Math.Abs(j[JointType.HandRight].Position.Y - j[JointType.ElbowRight].Position.Y)
					&& j[JointType.ElbowRight].Position.Z - j[JointType.HandRight].Position.Z > 0.1)
				{
					return GesturePartResult.Succeed;
				}
				return GesturePartResult.Pausing;
			}
			return GesturePartResult.Fail;
		}
	}

	public class RightKozakiewiczSegment4 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			var j = skeleton.Joints;

			if (KozakiewiczPositions.ElbowsBeneathShoulders(skeleton))
			{
				// left hand still "on" right elbow
				// right hand up "on" right shoulder
				if (KozakiewiczPositions.LeftHandOnRightElbow(skeleton)
					&& 0.10 >= Math.Abs(j[JointType.WristRight].Position.X - j[JointType.ShoulderRight].Position.X)
					&& 0.10 >= Math.Abs(j[JointType.WristRight].Position.Y - j[JointType.ShoulderRight].Position.Y)
					&& j[JointType.ShoulderRight].Position.Z - j[JointType.WristRight].Position.Z <= 0.3)
				{
					return GesturePartResult.Succeed;
				}
				return GesturePartResult.Pausing;
			}
			return GesturePartResult.Fail;
		}
	}
}