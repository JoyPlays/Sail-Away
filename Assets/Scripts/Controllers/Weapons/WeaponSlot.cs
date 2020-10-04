using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
	#region Fields

	[SerializeField] private WeaponType weaponType;
	
	private Transform _transform;
	
	#endregion
	
    #region Properties

	public Transform Transform
	{
		get
		{
			if (!_transform)
			{
				_transform = transform;
			}

			return _transform;
		}
		set => _transform = value;
	}

	#endregion
}
