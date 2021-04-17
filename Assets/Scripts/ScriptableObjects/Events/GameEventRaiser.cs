using UnityEngine;
using Sirenix.OdinInspector;

namespace Unistoty.GameEvents
{
	[HideMonoScript]
	public class GameEventRaiser : MonoBehaviour
	{
		[ListDrawerSettings(Expanded = true)]
		[SerializeField] private GameEvent[] events = new GameEvent[0];

		public void RaiseAll()
		{
			for (int i = 0; i < events.Length; i++)
			{
				GameEvent gameEvent = events[i];

				if (gameEvent)
				{
					gameEvent.Raise();
				}
			}
		}
	}
}