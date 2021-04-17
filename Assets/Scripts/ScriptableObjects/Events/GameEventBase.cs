using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Unistoty.GameEvents
{
	/// <summary>
	/// A scriptable object that holds a unique game event type.
	/// </summary>
	[Serializable]
	public abstract class GameEventBase : ScriptableObject
	{
		public abstract Type UnityEventType { get; protected set; }

		/// <summary>
		/// Name of the event scriptable object.
		/// </summary>
		/// <value>
		/// Gets the value of the <see cref="Object.Name"/> property.
		/// </value>
		public string Name => name;

		/// <summary>
		/// When set to True, will log each invokation connected to this event.
		/// </summary>
		/// <value>
		/// Gets the value of the boolean field logToConsole.
		/// </value>       
		public bool LogToConsole => logToConsole;

		/// <summary>
		/// The list of listeners that this event will notify if it is raised.
		/// </summary>
		/// <value>
		/// Gets the value of the field eventListeners.
		/// </value>
		[ShowInInspector, PropertySpace, PropertyOrder(10), HideInEditorMode]
		[ListDrawerSettings(Expanded = true, NumberOfItemsPerPage = 50)]
		public List<GameEventListener> EventListeners => eventListeners;

		#region Fields

#if dUI_MANAGER
		/// <summary>
		/// Should a Doozy UI event be raised along with this event?
		/// </summary>
		[Tooltip("Should a Doozy UI event be raised along with this event?")]
		[SerializeField, Space, PropertyOrder(0), InlineButton(nameof(CopyEventName), "Copy Event Name")]
		protected bool raiseDoozyUIEvent = false;

		private void CopyEventName() => GUIUtility.systemCopyBuffer = this.name;
#endif

		/// <summary>
		/// When set to True, will log each invocation connected to this event.
		/// </summary>
		[Tooltip("When set to True, will log each invocation connected to this event.")]
		[SerializeField, Space, PropertyOrder(2)]
		protected bool logToConsole = true;

		/// <summary>
		/// The list of listeners that this event will notify if it is raised.
		/// </summary>
		[Tooltip("The list of listeners that this event will notify if it is raised.")]
		protected List<GameEventListener> eventListeners = null;

		#endregion

		#region Listeners

		/// <summary>
		/// Registers a new listener.
		/// </summary>
		/// <param name="listener">Listener to register.</param>
		public void RegisterListener(GameEventListener listener)
		{
			if (eventListeners == null)
			{
				eventListeners = new List<GameEventListener>();
			}

			if (!eventListeners.Contains(listener))
			{
				eventListeners.Add(listener);
			}
		}

		/// <summary>
		/// Unregisters an existing listeners.
		/// </summary>
		/// <param name="listener">Listener to unregister.</param>
		public void UnregisterListener(GameEventListener listener)
		{
			if (eventListeners.Contains(listener))
			{
				eventListeners[eventListeners.IndexOf(listener)] = null;
			}
		}

		#endregion

		private void OnEnable()
		{
			eventListeners = new List<GameEventListener>();
		}

		protected void OnRaised()
		{
#if dUI_MANAGER
			OnRaised(this.raiseDoozyUIEvent);
#else
			OnRaised(false);
#endif
		}

		protected void OnRaised(bool raiseDoozyEvent)
		{
			RaiseDoozyUIEvent(raiseDoozyEvent);

			if (logToConsole && EventListeners.Count == 0)
			{
				LogZeroListeners(raiseDoozyEvent);
			}
		}

		private void LogZeroListeners(bool doozy)
		{
			if (doozy)
			{
				Debug.Log($"{name} event was raised for Doozy UI and zero listeners in scene");
			}
			else
			{
				Debug.Log($"{name} event was raised with zero listeners");
			}
		}

		private void RaiseDoozyUIEvent(bool raise)
		{
#if dUI_MANAGER
			if (raise)
			{
				Doozy.Engine.Message.Send(new Doozy.Engine.GameEventMessage(this.name));
			}
#endif
		}
	}
}