using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unistoty.GameEvents
{
#if UNITY_EDITOR
	[CreateAssetMenu(fileName = "New Event (GameObject)", menuName = "Events/New Event (GameObject)", order = 1)]
#endif
	[Serializable]
	public class GameEventGameObject : GameEvent<GameObject>
	{
		public override Type UnityEventType { get; protected set; } = typeof(UnityEventGameObject);
	}

	[Serializable]
	public class UnityEventGameObject : UnityEvent<GameObject> { }
}