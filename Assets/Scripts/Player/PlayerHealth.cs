using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
	#region Fields

	[SerializeField] private GameEvent onPlayerDied;
	
	#endregion
	
	#region Properties

	public bool IsAlive { get; set; } = true;
	
	public float HitPoints { get; set; }
	
	public float MaxHitPoints { get; set; }
	#endregion

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			TakeDamage(10);
		}
	}

	public void TakeDamage(float damage)
	{
		if (!IsAlive)
		{
			return;
		}
		
		HitPoints -= damage;
		DamageResponse();


		if (HitPoints <= 0)
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
		IsAlive = false;
		onPlayerDied.Raise();
		Debug.Log("Player hit points lost");
	}
}
