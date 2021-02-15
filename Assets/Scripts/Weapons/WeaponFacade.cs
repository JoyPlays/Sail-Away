﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFacade : MonoBehaviour, IShooter, IUpdateable
{
	[SerializeField] private WeaponDataSO weaponData;
	[SerializeField] private WeaponAimController weaponAimController;
	[SerializeField] private Transform shootPoint;
	[SerializeField] private Transform bodyPoint;

	private float nextShootTime = 0;
	
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
		if (weaponAimController.Target)
		{
			if (Time.time >= nextShootTime && weaponAimController.TargetLocked)
			{
				// TODO: Move to separate shootController
				nextShootTime = Time.time + (1 / weaponData.FireRate);
				Shoot();
			}
		}

		// if (Input.GetKeyDown(KeyCode.Space))
		// {
		// 	Shoot();
		// }
	}

	public void Shoot()
	{
		GameObject cannonBallObj = ObjectPooler.Instance.SpawnFromPool("Cannonball", ShootPoint.position, ShootPoint.rotation);
		
		if (cannonBallObj.TryGetComponent(out Rigidbody rBody))
		{
			rBody.velocity = GetBallisticVelocityVector(ShootPoint.position, weaponAimController.Target.position);//rBody.transform.forward * 10f;
		}
	}
	
	private Vector3 GetBallisticVelocityVector(Vector3 source, Vector3 target)
	{
		target.y = ShootPoint.position.y;
		
		float firingAngle = Vector3.Angle(ShootPoint.forward, bodyPoint.forward);
		float target_Distance = Vector3.Distance(ShootPoint.position, target) - ShootPoint.position.y;
		
		float projectile_Velocity = Mathf.Sqrt(target_Distance * Physics.gravity.magnitude / Mathf.Sin(2 * (firingAngle * Mathf.Deg2Rad)));


		return ShootPoint.forward * projectile_Velocity;
	}
	

	private void OnDestroy()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.DeregisterUpdateableObject(this);
		}
	}

	
}