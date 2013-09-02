using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SendKeyByGesture
{
	public class ReturnGestureCoordinator
	{
		public ReturnGestureCoordinator()
		{
			lastGestureDueDateTimer = new Timer(3000);
			lastGestureDueDateTimer.Elapsed += (_, __) =>
			{
				lastGesture = "";
				lastGestureDueDateTimer.Stop();
			};
			
			returnGesturePairs = new []
			{
				new[] {GesturesRegistry.LeftHandSwipeLeft, GesturesRegistry.LeftHandSwipeRight},
				new[] {GesturesRegistry.RightHandSwipeLeft, GesturesRegistry.RightHandSwipeRight}
			};
		}


		public bool CancelReturnGesture(string gesture)
		{
			if (lastGesture == "")
			{
				lastGesture = gesture;
				return false;
			}

			lastGestureDueDateTimer.Stop();
			lastGestureDueDateTimer.Start();
			var isReturnGesture = returnGesturePairs.Any(p => p[0] == gesture && p[1] == lastGesture || p[1] == gesture && p[0] == lastGesture);
			if (!isReturnGesture)
			{
				lastGesture = gesture;
			}
			return isReturnGesture;
		}


		private string lastGesture;
		private readonly Timer lastGestureDueDateTimer;
		private readonly IEnumerable<string[]> returnGesturePairs;
	}
}