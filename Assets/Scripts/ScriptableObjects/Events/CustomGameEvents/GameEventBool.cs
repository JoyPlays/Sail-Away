using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unistoty.GameEvents
{
#if UNITY_EDITOR
	[CreateAssetMenu(fileName = "New Event (Bool)", menuName = "Events/New Event (Bool)", order = 1)]
#endif
	[Serializable]
	public class GameEventBool : GameEvent<bool>
	{
		public override Type UnityEventType { get; protected set; } = typeof(UnityEventBool);
	}

	[Serializable]
	public class UnityEventBool : UnityEvent<bool> { }
}