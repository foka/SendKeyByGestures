using Fizbin.Kinect.Gestures;
using Microsoft.Kinect;

namespace SendKeyByGesture.Gestures.Segments
{
	// Seated-mode-ready swipe-left gesture made with right hand


	public class RightHandSwipeLeftSegment1 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			// right hand in front of right elbow
			if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
			{
				// right hand below head height
				if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
				{
					// right hand right of right elbow
					if (skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ElbowRight].Position.X)
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

	public class RightHandSwipeLeftSegment2 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			// right hand in front of right shoulder
			if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
			{
				// right hand below head height 
				if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.Head].Position.Y)
				{
					// right hand left of right shoulder & right of left shoulder
					if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderRight].Position.X && skeleton.Joints[JointType.HandRight].Position.X > skeleton.Joints[JointType.ShoulderLeft].Position.X)
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

	public class RightHandSwipeLeftSegment3 : IRelativeGestureSegment
	{
		public GesturePartResult CheckGesture(Skeleton skeleton)
		{
			// right hand in front of right elbow
			if (skeleton.Joints[JointType.HandRight].Position.Z < skeleton.Joints[JointType.ElbowRight].Position.Z && skeleton.Joints[JointType.HandLeft].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
			{
				// right hand below head height
				if (skeleton.Joints[JointType.HandRight].Position.Y < skeleton.Joints[JointType.ShoulderCenter].Position.Y)
				{
					// right hand left of shoulder center
					if (skeleton.Joints[JointType.HandRight].Position.X < skeleton.Joints[JointType.ShoulderCenter].Position.X)
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