using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

public class WeaponFacade : SerializedMonoBehaviour, IUpdateable
{
	[SerializeField] private WeaponDataSO weaponData;
	[SerializeField] private WeaponAimController weaponAimController;
	[SerializeField] private IShooter shooter;
	
	[SerializeField] private UnityEvent shootEvent;

	private float nextShootTime = 0;
	public bool Enabled => enabled;

	private void Start()
	{
		weaponAimController.Setup(weaponData);
		shooter.Setup(weaponAimController, weaponData);
		if (UpdateController.Instance)
		{
			UpdateController.Instance.RegisterUpdateableObject(this);
		}
	}
	
	public void OnUpdate(float deltaTime)
	{
		if (weaponAimController.Target != null)
		{
			if (Time.time >= nextShootTime && weaponAimController.TargetLocked)
			{
				nextShootTime = Time.time + (1 / weaponData.FireRate);
				shootEvent?.Invoke();
			}
		}
		
	}

	
	
	
	

	private void OnDestroy()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.DeregisterUpdateableObject(this);
		}
	}

	
}
