using Sirenix.OdinInspector;
using System;

/// <summary>
/// A scriptable object that holds a unique game event type.
/// </summary>
[Serializable]
public abstract class GameEvent<T0> : GameEventBase
{
	#region Raise

	/// <summary>
	/// Raises the event, notifying all listeners.
	/// </summary>
	[Button("Raise"), PropertyOrder(5), DisableInEditorMode, PropertySpace]
	public void Raise(T0 arg0)
	{
		for (int index = EventListeners.Count - 1; index >= 0; index--)
		{
			GameEventListener currentListener = eventListeners[index];

			if (currentListener == null)
			{
				eventListeners.RemoveAt(index);
				continue;
			}

			currentListener.OnEventRaised(this, arg0);
		}

		OnRaised();
	}

	#endregion
}