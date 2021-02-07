using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFacade : MonoBehaviour, IShooter
{
	[SerializeField] private WeaponDataSO weaponData;
	
	[SerializeField] private Transform shootPoint;

	public Transform ShootPoint => shootPoint;

	public void Update()
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
}
