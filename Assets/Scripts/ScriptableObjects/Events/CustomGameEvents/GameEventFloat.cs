using System;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "New Event (Float)", menuName = "Events/New Event (Float)", order = 1)]
#endif
[Serializable]
public class GameEventFloat : GameEvent<float>
{
	public override Type UnityEventType { get; protected set; } = typeof(UnityEventFloat);
}

[Serializable]
public class UnityEventFloat : UnityEvent<float>
{
}