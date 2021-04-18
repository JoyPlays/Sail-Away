using Sirenix.OdinInspector;
using UnityEngine;
using System;

/// <summary>
/// A scriptable object that holds an integer value and raises an optional event when that value changes.
/// </summary>
[Serializable]
[CreateAssetMenu(fileName = "Int Variable", menuName = "Scriptable Objects/Variables/Int Variable", order = 1)]
public class IntVariable : ScriptableObject
{
	#region Properties

	/// <summary>
	/// Value of the integer variable.
	/// </summary>
	/// <value>
	/// Gets and sets the value of the integer field intValue. Raises an event if one is assigned in the <see cref="eventToRaise"/> field.
	/// </value>
	public int Value
	{
		get => currentValue;

		set
		{
			currentValue = Clamp(value);
			eventToRaiseOnCurrentValueChange?.Raise();
		}
	}

	/// <summary>
	/// Maximum allowed value of this variable.
	/// </summary>
	/// <value>
	/// Gets and sets the value of the int field maxValue.
	/// </value>
	public int MaxValue
	{
		get => maxValue;

		set
		{
			maxValue = value;
			eventToRaiseOnMaxValueChange?.Raise();
		}
	}

	/// <summary>
	/// Minimum allowed value of this variable.
	/// </summary>
	/// <value>
	/// Gets and sets the value of the int field minValue.
	/// </value>
	public int MinValue
	{
		get => minValue;

		set
		{
			minValue = value;
			eventToRaiseOnMinValueChange?.Raise();
		}
	}

	/// <summary>
	/// Has this variable reached its maximum value?
	/// </summary>        
	/// <value>
	/// Gets a boolean result of comparing field currentValue and maxValue.
	/// </value>
	public bool IsMaxValue => currentValue == maxValue;

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
	/// Minimum allowed value of this variable.
	/// </summary>
	[Title("Values")] [Tooltip("Minimum allowed value of this variable.")] [SerializeField]
	private int minValue = 0;

	/// <summary>
	/// Maximum allowed value of this variable.
	/// </summary>
	[Tooltip("Maximum allowed value of this variable.")] [SerializeField]
	private int maxValue = 0;

	/// <summary>
	/// Current value of this variable.
	/// </summary>
	[ReadOnly] [SerializeField] private int currentValue;

	/// <summary>
	/// Start value type for this variable.
	/// </summary>
	[Tooltip("Start value type for this variable.")] [SerializeField, EnumPaging]
	private VariableStartValueType variableStartValueType = VariableStartValueType.Default;

	/// <summary>
	/// Custom start value to use if <see cref="variableStartValueType"/> is set to <see cref="VariableStartValueType.Custom"/>.
	/// </summary>
	[Tooltip("Custom start value to use if variableStartValueTypeis set to VariableStartValueType.Custom")] [SerializeField, HideIf("@this.variableStartValueType != VariableStartValueType.Custom")]
	private int customStartValue = 0;

	/// <summary>
	/// Optional event to raise when this variable's current value changes.
	/// </summary>
	[Title("Events")] [Tooltip("Optional event to raise when this variable's value changes.")] [SerializeField]
	private GameEvent eventToRaiseOnCurrentValueChange = null;

	/// <summary>
	/// Optional event to raise when this variable's minimum value changes.
	/// </summary>
	[Tooltip("Optional event to raise when this variable's minimum value changes.")] [SerializeField]
	private GameEvent eventToRaiseOnMinValueChange = null;

	/// <summary>
	/// Optional event to raise when this variable's maximum value changes.
	/// </summary>
	[Tooltip("Optional event to raise when this variable's maximum value changes.")] [SerializeField]
	private GameEvent eventToRaiseOnMaxValueChange = null;

	#endregion

	#region Init

	private void OnEnable()
	{
		switch (variableStartValueType)
		{
			case VariableStartValueType.Default:
				currentValue = default;
				break;
			case VariableStartValueType.Min:
				currentValue = minValue;
				break;
			case VariableStartValueType.Max:
				currentValue = maxValue;
				break;
			case VariableStartValueType.Custom:
				currentValue = customStartValue;
				break;
		}
	}

	#endregion

	#region Value

	/// <summary>
	/// Sets value to the value of the passed <see cref="IntVariable"/>.
	/// </summary>
	/// <param name="value">IntVariable containing new value.</param>
	public void Set(IntVariable value) => Value = value.Value;

	/// <summary>
	/// Sets value to the value of the passed Integer.
	/// </summary>
	/// <param name="value">Integer containing new value.</param>
	public void Set(int value) => Value = value;

	/// <summary>
	/// Adds an integer to current value.
	/// </summary>
	/// <param name="amount">Integer value to add.</param>
	public void Add(int amount) => Value += amount;

	/// <summary>
	/// Adds the value of the passed <see cref="IntVariable"/> to current value.
	/// </summary>
	/// <param name="amount">IntVariable whose value needs to be added.</param>
	public void Add(IntVariable amount) => Value += amount.Value;

	/// <summary>
	/// Clamps the value between <see cref="minValue"/> and <see cref="maxValue"/>. If both are 0, returns the passed value.
	/// </summary>
	/// <param name="value">Value to clamp.</param>
	/// <returns>A integer.</returns>
	private int Clamp(int value)
	{
		return (minValue == 0 && maxValue == 0) ? value : Mathf.Clamp(value, minValue, maxValue);
	}

	#endregion

	private void OnValidate() => Value = currentValue;
}