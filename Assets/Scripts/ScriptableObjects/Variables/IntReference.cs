using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace Unistoty.SOVariables
{
	/// <summary>
	/// Stores a reference to a <see cref="IntVariable"/>, allowing to swap the value of the referenced variable
	/// with a constants value for debugging/special conditions.
	/// </summary>
	[Serializable]
	public class IntReference
	{
		#region Properties

		/// <summary>
		/// The value of the integer reference which is either the value of the referenced <see cref="IntVariable"/>
		/// or a constant value.
		/// </summary>
		/// <value>
		/// Gets the value of <see cref="constantValue"/> if <see cref="useConstant"/> is set to True, 
		/// value stored in <see cref="variable"/> otherwise.
		/// </value>
		public int Value => useConstant ? constantValue : variable.Value;

		#endregion

		#region Fields

		/// <summary>
		/// Integer variable this object references.
		/// </summary>
		[Tooltip("Float variable this object references.")]
		[SerializeField]
		private IntVariable variable = null;

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
		private int constantValue = 0;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="constantValue">Constant value to use for this reference.</param>
		public IntReference(int constantValue)
		{
			useConstant = true;
			this.constantValue = constantValue;
		}

		#endregion

		#region Value

		/// <summary>
		/// Implicit converstion to int.
		/// </summary>
		/// <param name="reference">Reference to convert.</param>
		public static implicit operator int(IntReference reference) => reference.Value;

		#endregion
	}
}