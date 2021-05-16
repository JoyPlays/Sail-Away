using UnityEngine;

public interface IShooter
{
	WeaponAimController WeaponAimController { get; }
	WeaponDataSO WeaponData { get; }
	Transform ShootPoint { get; }

	void Setup(WeaponAimController weaponAimController, WeaponDataSO weaponData);

	void Shoot();
}
