using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace SendKeyByGesture.Gestures
{
	public class ReturnGestureCoordinator
	{
		private const int gestureLifetime = 3000;
		private static readonly IEnumerable<string[]> returnGesturePairs = new []
		{
			new[] {GesturesRegistry.HL_SwipeLeft, GesturesRegistry.HL_SwipeRight},
			new[] {GesturesRegistry.HR_SwipeLeft, GesturesRegistry.HR_SwipeRight}
		};

		// key: trackingId
		private readonly IDictionary<int, PlayerCoordinator> playerCoordinators;


		public ReturnGestureCoordinator()
		{
			playerCoordinators = new Dictionary<int, PlayerCoordinator>();
		}

		public bool CancelReturnGesture(string gesture, int trackingId)
		{
			PlayerCoordinator playerCoordinator;
			lock (playerCoordinators)
			{
				playerCoordinator = playerCoordinators.ContainsKey(trackingId) ? playerCoordinators[trackingId] : (playerCoordinators[trackingId] = new PlayerCoordinator());
			}

			return playerCoordinator.CancelReturnGesture(gesture);
		}



		private class PlayerCoordinator
		{
			public PlayerCoordinator()
			{
				lastGestureDueDateTimer = new Timer(gestureLifetime);
				lastGestureDueDateTimer.Elapsed += (_, __) =>
				{
					lastGesture = "";
					lastGestureDueDateTimer.Stop();
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
		}
	}
}