using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// A scriptable object that holds a unique game event type.
/// </summary>
#if UNITY_EDITOR
[CreateAssetMenu(fileName = "New Event", menuName = "Events/New Event", order = 0)]
#endif
[Serializable]
public class GameEvent : GameEventBase
{
	public override Type UnityEventType { get; protected set; } = typeof(UnityEvent);

	#region Raise

	/// <summary>
	/// Raises the event, notifying all listeners.
	/// </summary>
	[Button("Raise"), PropertyOrder(5), DisableInEditorMode, PropertySpace]
	public void Raise()
	{
#if dUI_MANAGER
			RaiseInternal(this.raiseDoozyUIEvent);
#else
		RaiseInternal(false);
#endif
	}

#if dUI_MANAGER
		/// <summary>
		/// Raise the event, with overridable value for notifying also the Doozy UI event system
		/// </summary>
		public void Raise(bool raiseDoozyEvent)
		{
			RaiseInternal(raiseDoozyEvent);
		}
#endif

	private void RaiseInternal(bool raiseDoozyEvent)
	{
		for (int index = EventListeners.Count - 1; index >= 0; index--)
		{
			GameEventListener currentListener = eventListeners[index];

			if (currentListener == null)
			{
				eventListeners.RemoveAt(index);
				continue;
			}

			currentListener.OnEventRaised(this);
		}

		OnRaised(raiseDoozyEvent);
	}

	#endregion
}