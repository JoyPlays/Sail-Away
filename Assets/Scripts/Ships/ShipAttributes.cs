using UnityEngine;

[CreateAssetMenu(fileName = "Ship Attributes", menuName = "Ships/Ship Attributes")]
public class ShipAttributes : ScriptableObject
{
	#region Fields
	
	[SerializeField] private string shipName;
	[SerializeField] private int cargoHold;
	[SerializeField] private float turnRate;
	[SerializeField] private float speed;
	[SerializeField] private int hitPoint;
	[SerializeField] private int viewRange;
	
	#endregion
	
	#region Properties
	
	public string ShipName => shipName;

	public int CargoHold => cargoHold;

	public float TurnRate => turnRate;

	public float Speed => speed;

	public int HitPoints => hitPoint;

	public int ViewRange => viewRange;
	
	#endregion
}
