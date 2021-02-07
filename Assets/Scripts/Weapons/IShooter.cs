using UnityEngine;

public interface IShooter
{
	Transform ShootPoint { get; }

	void Shoot();
}
