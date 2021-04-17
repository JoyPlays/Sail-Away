using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Unistoty.SOVariables
{
	/// <summary>
	/// Stores a reference to a <see cref="FloatVariable"/>, allowing to swap the value of the referenced variable
	/// with a constants value for debugging/special conditions.
	/// </summary>
	[Serializable]
	public class FloatReference
	{
		#region Properties

		/// <summary>
		/// The value of the float reference which is either the value of the references <see cref="FloatVariable"/>
		/// or a constant value.
		/// </summary>
		/// <value>
		/// Gets the value of <see cref="constantValue"/> if <see cref="useConstant"/> is set to True, 
		/// value stored in <see cref="variable"/> otherwise.
		/// </value>
		public float Value => useConstant ? constantValue : variable.Value;

		#endregion

		#region Fields

		/// <summary>
		/// Float variable this object references.
		/// </summary>
		[Tooltip("Float variable this object references.")]
		[SerializeField]
		private FloatVariable variable = null;

		/// <summary>
		/// Should a constant value be used instead of the value stored in <see cref="variable"/>?
		/// </summary>
		[Tooltip("Should a constant value be used instead of the value stored in Variable field?")]
		[SerializeField]
		private bool useConstant = false;


		/// <summary>
		/// Value to use if <see cref="useConstant"/> is True.
		/// </summary>
		[Tooltip("Value to use if Use Constant is True.")]
		[SerializeField, HideIf("@this.useConstant == true")]
		private float constantValue = 0f;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="constantValue">Constant value to use for this reference.</param>
		public FloatReference(float constantValue)
		{
			useConstant = true;
			this.constantValue = constantValue;
		}

		#endregion

		#region Value

		/// <summary>
		/// Implicit converstion to float.
		/// </summary>
		/// <param name="reference">Reference to convert.</param>
		public static implicit operator float(FloatReference reference) => reference.Value;

		#endregion
	}
}