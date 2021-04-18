using System;
using UnityEngine;
using UnityEngine.Events;

#if UNITY_EDITOR
[CreateAssetMenu(fileName = "New Event (String)", menuName = "Events/New Event (String)", order = 1)]
#endif
[Serializable]
public class GameEventString : GameEvent<string>
{
	public override Type UnityEventType { get; protected set; } = typeof(UnityEventString);
}

[Serializable]
public class UnityEventString : UnityEvent<string>
{
}