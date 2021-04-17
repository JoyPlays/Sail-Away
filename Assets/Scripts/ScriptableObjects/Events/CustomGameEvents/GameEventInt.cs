using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unistoty.GameEvents
{
#if UNITY_EDITOR
	[CreateAssetMenu(fileName = "New Event (Int)", menuName = "Events/New Event (Int)", order = 1)]
#endif
	[Serializable]
	public class GameEventInt : GameEvent<int>
	{
		public override Type UnityEventType { get; protected set; } = typeof(UnityEventInt);
	}

	[Serializable]
	public class UnityEventInt : UnityEvent<int> { }
}