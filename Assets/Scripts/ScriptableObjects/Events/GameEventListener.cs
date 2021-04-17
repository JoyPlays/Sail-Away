using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Unistoty.GameEvents
{
    /// <summary>
    /// Stores a list of events to listen to and respective responses.
    /// </summary>
    [Serializable]
#if UNITY_EDITOR
    [ExecuteAlways]
#endif
    [HideMonoScript]
    public class GameEventListener : MonoBehaviour
    {
        #region Properties

        /// <summary>
        /// Array of event-response pairs belonging to this listener.
        /// </summary>
        /// <value>
        /// Gets the value of the field eventResponsePairs.
        /// </value>
        public EventResponsePair[] EventReponsePairs => (EventResponsePair[])eventResponsePairs.Clone();

        #endregion

        #region Fields

        /// <summary>
        /// Array of event-response pairs belonging to this listener.
        /// </summary>
        [Tooltip("Array of event-response pairs belonging to this listener.")]
        [SerializeField]
        [ListDrawerSettings(Expanded = true)]
        private EventResponsePair[] eventResponsePairs = null;

        #endregion

        #region Enable/disable

        private void OnValidate()
        {
            if (eventResponsePairs != null)
            {
                for (int i = 0; i < eventResponsePairs.Length; i++)
                {
                    EventResponsePair e = eventResponsePairs[i];
                    if (e != null)
                    {
                        e.OnValidate();
                    }
                }
            }
        }

        private void Awake() => OnEnable();

        private void OnEnable()
        {
            if (!enabled)
            {
                return;
            }

            if (eventResponsePairs == null)
            {
                return;
            }

            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair e = eventResponsePairs[i];
                if (e.Event == null || e.Event.EventListeners == null || e.Event.EventListeners.Contains(this))
                {
                    continue;
                }

                if (e.Event != null)
                {
                    e.Event.RegisterListener(this);
                }
            }
        }

        private void OnDisable()
        {
            if (eventResponsePairs == null)
            {
                return;
            }

            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair e = eventResponsePairs[i];
                if (e.Event != null)
                {
                    e.Event.UnregisterListener(this);
                }
            }
        }

        #endregion

        #region Event

        /// <summary>
        /// Invokes listeners if their event's name matches the name of the passed argument.
        /// </summary>
        /// <param name="e">Target event.</param>
        public void OnEventRaised(GameEventBase e)
        {
            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair eventResponsePair = eventResponsePairs[i];
                if (e.Equals(eventResponsePair.Event))
                {
                    eventResponsePair.LogToConsoleIfEnabled(this);
                    eventResponsePair.Invoke();
                }
            }
        }

        public void OnEventRaised<T0>(GameEventBase e, T0 arg0)
        {
            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair eventResponsePair = eventResponsePairs[i];
                if (e.Equals(eventResponsePair.Event))
                {
                    eventResponsePair.LogToConsoleIfEnabled(this);
                    eventResponsePair.Invoke(arg0);
                }
            }
        }

        public void OnEventRaised<T0, T1>(GameEventBase e, T0 arg0, T1 arg1)
        {
            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair eventResponsePair = eventResponsePairs[i];
                if (e.Equals(eventResponsePair.Event))
                {
                    eventResponsePair.LogToConsoleIfEnabled(this);
                    eventResponsePair.Invoke(arg0, arg1);
                }
            }
        }

        public void OnEventRaised<T0, T1, T2>(GameEventBase e, T0 arg0, T1 arg1, T2 arg2)
        {
            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair eventResponsePair = eventResponsePairs[i];
                if (e.Equals(eventResponsePair.Event))
                {
                    eventResponsePair.LogToConsoleIfEnabled(this);
                    eventResponsePair.Invoke(arg0, arg1, arg2);
                }
            }
        }

        public void OnEventRaised<T0, T1, T2, T3>(GameEventBase e, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
        {
            for (int i = 0; i < eventResponsePairs.Length; i++)
            {
                EventResponsePair eventResponsePair = eventResponsePairs[i];
                if (e.Equals(eventResponsePair.Event))
                {
                    eventResponsePair.LogToConsoleIfEnabled(this);
                    eventResponsePair.Invoke(arg0, arg1, arg2, arg3);
                }
            }
        }

        #endregion
    }
}