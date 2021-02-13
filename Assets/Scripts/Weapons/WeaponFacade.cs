using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFacade : MonoBehaviour, IShooter, IUpdateable
{
	[SerializeField] private WeaponDataSO weaponData;
	[SerializeField] private WeaponAimController weaponAimController;
	[SerializeField] private Transform shootPoint;

	public Transform ShootPoint => shootPoint;
	
	private void Start()
	{
		weaponAimController.Setup(weaponData);
		
		if (UpdateController.Instance)
		{
			UpdateController.Instance.RegisterUpdateableObject(this);
		}
	}
	
	public void OnUpdate(float deltaTime)
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Shoot();
		}
	}

	public void Shoot()
	{
		GameObject cannonBallObj = ObjectPooler.Instance.SpawnFromPool("Cannonball", ShootPoint.position, ShootPoint.rotation);
		
		if (cannonBallObj.TryGetComponent(out Rigidbody rBody))
		{
			rBody.velocity = rBody.transform.forward * 10f;
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
