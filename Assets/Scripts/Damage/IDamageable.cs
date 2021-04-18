using UnityEngine;

public interface IDamageable
{
	bool IsAlive { get; }
	float HitPoints { get; }
	float MaxHitPoints { get; }
	
	void TakeDamage(float damage);
	void DamageResponse();
	void OnAllHitPointsLost();
}