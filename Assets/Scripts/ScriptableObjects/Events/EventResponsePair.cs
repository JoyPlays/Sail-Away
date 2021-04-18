using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

/// <summary>
/// Holds a pair of event and respective response.
/// </summary>
[Serializable]
public class EventResponsePair
{
	#region Properties

	/// <summary>
	/// <see cref="GameEvent"/> to register a response with.
	/// </summary>
	/// <value>
	/// Gets the value of the field @event.
	/// </value>
	public GameEventBase Event => @event;

	#endregion

	#region Fields

	/// <summary>
	/// <see cref="GameEvent"/> to register a response with.
	/// </summary>
	[Tooltip("GameEvent to register a response with.")] [SerializeField]
	private GameEventBase @event = null;

	/// <summary>
	/// Response to invoke when <see cref="GameEvent"/> is raised.
	/// </summary>
	[Tooltip("Response to invoke when GameEvent is raised.")] [SerializeField, ShowIf("@(this.Event is " + nameof(GameEvent) + ")", animate: false)]
	private UnityEvent response = null;

	// Custom Unity Events
	[SerializeField, ShowIf("@(this.Event is " + nameof(GameEventGameObject) + ")", animate: false)]
	private UnityEventGameObject responseGameObject = null;

	[SerializeField, ShowIf("@(this.Event is " + nameof(GameEventTransform) + ")", animate: false)]
	private UnityEventTransform responseTransform = null;

	[SerializeField, ShowIf("@(this.Event is " + nameof(GameEventInt) + ")", animate: false)]
	private UnityEventInt responseInt = null;

	[SerializeField, ShowIf("@(this.Event is " + nameof(GameEventFloat) + ")", animate: false)]
	private UnityEventFloat responseFloat = null;

	[SerializeField, ShowIf("@(this.Event is " + nameof(GameEventBool) + ")", animate: false)]
	private UnityEventBool responseBool = null;

	[SerializeField, ShowIf("@(this.Event is " + nameof(GameEventString) + ")", animate: false)]
	private UnityEventString responseString = null;

	#endregion

	#region Invoke

	public void Invoke()
	{
		if (Event == null) return;
		response.Invoke();
	}

	// Invoke handlers for Custom Unity Events
	public void Invoke<T>(T arg0)
	{
		if (Event == null) return;
		if (Event.UnityEventType == typeof(UnityEventGameObject) && arg0 is GameObject gameObject) responseGameObject.Invoke(gameObject);
		else if (Event.UnityEventType == typeof(UnityEventTransform) && arg0 is Transform transform) responseTransform.Invoke(transform);
		else if (Event.UnityEventType == typeof(UnityEventInt) && arg0 is int intValue) responseInt.Invoke(intValue);
		else if (Event.UnityEventType == typeof(UnityEventFloat) && arg0 is float floatValue) responseFloat.Invoke(floatValue);
		else if (Event.UnityEventType == typeof(UnityEventBool) && arg0 is bool boolValue) responseBool.Invoke(boolValue);
		else if (Event.UnityEventType == typeof(UnityEventString) && arg0 is string stringValue) responseString.Invoke(stringValue);
	}

	public void Invoke<T0, T1>(T0 arg0, T1 arg1)
	{
		if (Event == null) return;
	}

	public void Invoke<T0, T1, T2>(T0 arg0, T1 arg1, T2 arg2)
	{
		if (Event == null) return;
	}

	public void Invoke<T0, T1, T2, T3>(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
	{
		if (Event == null) return;
	}

	#endregion

	#region Validation

	// Custom Unity Event validation
	public void OnValidate()
	{
		response = Event != null && Event.UnityEventType == typeof(UnityEvent) ? response : null;

		responseGameObject = Event != null && Event.UnityEventType == typeof(UnityEventGameObject) ? responseGameObject : null;
		responseTransform = Event != null && Event.UnityEventType == typeof(UnityEventTransform) ? responseTransform : null;
		responseInt = Event != null && Event.UnityEventType == typeof(UnityEventInt) ? responseInt : null;
		responseFloat = Event != null && Event.UnityEventType == typeof(UnityEventFloat) ? responseFloat : null;
		responseBool = Event != null && Event.UnityEventType == typeof(UnityEventBool) ? responseBool : null;
		responseString = Event != null && Event.UnityEventType == typeof(UnityEventString) ? responseString : null;
	}

	#endregion

	#region Custom Logging

	private void LogToConsole(GameEventListener listener)
	{
		if (Event.UnityEventType == typeof(UnityEvent)) PrintLog(listener, response, Event);
		else if (Event.UnityEventType == typeof(UnityEventGameObject)) PrintLog(listener, responseGameObject, Event);
		else if (Event.UnityEventType == typeof(UnityEventTransform)) PrintLog(listener, responseTransform, Event);
		else if (Event.UnityEventType == typeof(UnityEventInt)) PrintLog(listener, responseInt, Event);
		else if (Event.UnityEventType == typeof(UnityEventFloat)) PrintLog(listener, responseFloat, Event);
		else if (Event.UnityEventType == typeof(UnityEventBool)) PrintLog(listener, responseBool, Event);
		else if (Event.UnityEventType == typeof(UnityEventString)) PrintLog(listener, responseString, Event);
	}

	#endregion

	#region Logging

	public void LogToConsoleIfEnabled(GameEventListener eventListener)
	{
#if UNITY_EDITOR
		if (Event.LogToConsole)
		{
			LogToConsole(eventListener);
		}
#endif
	}

	private static void PrintLog(GameEventListener eventListener, UnityEventBase unityEvent, GameEventBase gameEvent)
	{
		int persistentEventCount = unityEvent.GetPersistentEventCount();
		if (persistentEventCount == 0)
		{
			Debug.Log(string.Format("<i>{0}</i> event was raised, but no persistent listeners were found on <b>{1}</b>",
				gameEvent.name, eventListener.gameObject.name));
		}
		else
		{
			Object persistentTarget = unityEvent.GetPersistentTarget(0);

			string invokedOn = persistentTarget ? persistentTarget.name : null;
			string handlerMethod = unityEvent.GetPersistentMethodName(0);
			string eventName = gameEvent.name;

			PrintLog(invokedOn, handlerMethod, eventName, eventListener);
		}
	}

	private static void PrintLog(string invokedOn, string handlerMethod, string eventName, GameEventListener eventListener)
	{
		Debug.Log(string.Format("<b>{0}</b> is invoking <i>{1}</i> in response to <i>{2}</i> event", invokedOn, handlerMethod, eventName), context: eventListener);
	}

	#endregion
}