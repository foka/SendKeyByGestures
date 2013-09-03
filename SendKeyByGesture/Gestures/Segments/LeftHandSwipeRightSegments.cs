using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace SendKeyByGesture.Gestures.Segments
{
	// Seated-mode-ready swipe-right gesture made with left hand


	public class LeftHandSwipeRightSegment1 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			// left hand in front of left elbow
			if (skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
			{
				// left hand below head height
				if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
				{
					// left hand left of left elbow
					if (skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ElbowLeft].Position.X)
					{
						return GesturePartResult.Succeed;
					}
					return GesturePartResult.Pausing;
				}
				return GesturePartResult.Fail;
			}
			return GesturePartResult.Fail;
		}
	}

	public class LeftHandSwipeRightSegment2 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			// left hand in front of elbow
			if (skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
			{
				// left hand below head height
				if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
				{
					// left hand right of left shoulder & left of right shoulder
					if (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X && skeleton.Joints[JointType.HandLeft].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X)
					{
						return GesturePartResult.Succeed;
					}
					return GesturePartResult.Pausing;
				}
				return GesturePartResult.Fail;
			}
			return GesturePartResult.Fail;
		}
	}

	public class LeftHandSwipeRightSegment3 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			// left hand in front of left elbow
			if (skeleton.Joints[JointType.HandLeft].Position.Z < skeleton.Joints[JointType.ElbowLeft].Position.Z && skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
			{
				// left hand below head height
				if (skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
				{
					// left hand right of shoulder center
					if (skeleton.Joints[JointType.HandLeft].Position.X > skeleton.Joints[JointType.ShoulderCenter].Position.X)
					{
						return GesturePartResult.Succeed;
					}

					return GesturePartResult.Pausing;
				}

				return GesturePartResult.Fail;
			}

			return GesturePartResult.Fail;
		}
	}
}