using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A scriptable object that holds a Vector3 value.
/// </summary>
[CreateAssetMenu(fileName = "Vector3Variable", menuName = "Scriptable Objects/Variables/Vector3 Variable", order = 2)]
public class Vector3Variable : ScriptableObject
{
	#region Properties

	/// <summary>
	/// Value of the Vector3 variable.
	/// </summary>
	/// <value>
	/// Gets and sets the value of the Vector3 field currentValue. Raises an event if one is assigned in the <see cref="eventToRaise"/> field.
	/// </value>
	public Vector3 Value
	{
		get => currentValue;

		set
		{
			currentValue = value;

			if (eventToRaise != null)
			{
				eventToRaise.Raise();
			}
		}
	}

	#endregion

	#region Fields

#if UNITY_EDITOR
	/// <summary>
	/// Optional description to view in the inspector.
	/// </summary>
	[Multiline] [SerializeField]
#pragma warning disable CS0414
	private string description = "";
#pragma warning restore CS0414
#endif

	/// <summary>
	/// Optional event to raise when this variable's value changes.
	/// </summary>        
	[Tooltip("Event to raise when this variable's value changes.")] [SerializeField]
	private GameEvent eventToRaise = null;

	/// <summary>
	/// Current value of this variable.
	/// </summary>
	[ReadOnly] [SerializeField] private Vector3 currentValue;

	#endregion
}