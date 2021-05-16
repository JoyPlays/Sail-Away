using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonShootController : MonoBehaviour, IShooter
{
	[SerializeField] private AnimationCurve spreadCurve = AnimationCurve.Linear(0f,0f, 1f, 1f);
	[SerializeField] private Transform bodyPoint;
	[SerializeField] private Transform shootPoint;

	public WeaponAimController WeaponAimController { get; private set; }
	public WeaponDataSO WeaponData { get; private set; }
	public Transform ShootPoint => shootPoint;

	public void Setup(WeaponAimController weaponAimController, WeaponDataSO weaponData)
	{
		WeaponAimController = weaponAimController;
		WeaponData = weaponData;
	}

	public void Shoot()
	{
		GameObject cannonBallObj = ObjectPooler.Instance.SpawnFromPool("Cannonball", ShootPoint.position, ShootPoint.rotation);
		
		if (cannonBallObj.TryGetComponent(out Rigidbody rBody))
		{
			rBody.velocity = GetBallisticVelocityVector(WeaponAimController.Target.Transform.position);
		}
	}
	
	private Vector3 GetBallisticVelocityVector(Vector3 target)
	{
		float originalDistance = Vector3.Distance(target, ShootPoint.position);
		float t = Mathf.InverseLerp(2f, WeaponData.Range, originalDistance);

		float spreadValue = Mathf.Lerp(0, WeaponData.MaxSpread, spreadCurve.Evaluate(t));
		float spread = Random.Range(-spreadValue, spreadValue);
		
		Vector3 direction = (target - ShootPoint.position).normalized;
		target += direction * spread;
		target.y = ShootPoint.position.y;
		
		float firingAngle = Vector3.Angle(ShootPoint.forward, bodyPoint.forward);
		float target_Distance = Vector3.Distance(target, ShootPoint.position) - ShootPoint.position.y;
		
		float projectile_Velocity = Mathf.Sqrt(target_Distance * Physics.gravity.magnitude / Mathf.Sin(2 * (firingAngle * Mathf.Deg2Rad)));


		return ShootPoint.forward * projectile_Velocity;
	}
}
