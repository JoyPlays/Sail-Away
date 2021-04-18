using System;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "New Event (Transform)", menuName = "Events/New Event (Transform)", order = 1)]
#endif
[Serializable]
public class GameEventTransform : GameEvent<Transform>
{
	public override Type UnityEventType { get; protected set; } = typeof(UnityEventTransform);
}

[Serializable]
public class UnityEventTransform : UnityEvent<Transform>
{
}