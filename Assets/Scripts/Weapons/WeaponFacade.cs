using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFacade : MonoBehaviour, IShooter, IUpdateable
{
	[SerializeField] private AnimationCurve spreadCurve = AnimationCurve.Linear(0f,0f, 1f, 1f);
	[SerializeField] private WeaponDataSO weaponData;
	[SerializeField] private WeaponAimController weaponAimController;
	[SerializeField] private Transform shootPoint;
	[SerializeField] private Transform bodyPoint;

	private float nextShootTime = 0;
	private float maxSpread = 5;
	
	public Transform ShootPoint => shootPoint;
	
	private void Start()
	{
		maxSpread = weaponData.MaxSpread;
		weaponAimController.Setup(weaponData);
		
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
				// TODO: Move to separate shootController
				nextShootTime = Time.time + (1 / weaponData.FireRate);
				Shoot();
			}
		}
		
	}

	public void Shoot()
	{
		GameObject cannonBallObj = ObjectPooler.Instance.SpawnFromPool("Cannonball", ShootPoint.position, ShootPoint.rotation);
		
		if (cannonBallObj.TryGetComponent(out Rigidbody rBody))
		{
			rBody.velocity = GetBallisticVelocityVector(weaponAimController.Target.Transform.position);
		}
	}
	
	private Vector3 GetBallisticVelocityVector(Vector3 target)
	{
		float originalDistance = Vector3.Distance(target, ShootPoint.position);
		float t = Mathf.InverseLerp(2f, weaponData.Range, originalDistance);

		float spreadValue = Mathf.Lerp(0, maxSpread, spreadCurve.Evaluate(t));
		float spread = Random.Range(-spreadValue, spreadValue);
		
		Vector3 direction = (target - ShootPoint.position).normalized;
		target += direction * spread;
		target.y = ShootPoint.position.y;
		
		float firingAngle = Vector3.Angle(ShootPoint.forward, bodyPoint.forward);
		float target_Distance = Vector3.Distance(target, ShootPoint.position) - ShootPoint.position.y;
		
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
