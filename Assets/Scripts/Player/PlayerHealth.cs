using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	#region Fields

	[SerializeField] private GameEvent onPlayerDied;
	
	#endregion
	
	#region Properties
	public bool IsAlive => HitPoints > 0;
	
	public float HitPoints { get; set; }
	
	public float MaxHitPoints { get; set; }
	#endregion
	
	public void TakeDamage(float damage)
	{
		HitPoints -= damage;
		DamageResponse();

		if (!IsAlive)
		{
			OnAllHitPointsLost();
		}
	}

	public void DamageResponse()
	{
		Debug.Log("Player hit damage response");
	}

	public void OnAllHitPointsLost()
	{
		Debug.Log("Player hit points lost");
	}
}
