using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponDataSO : ScriptableObject
{
	#region Fields

	[SerializeField] private WeaponType type;
	[SerializeField] private float range;
	[SerializeField] private float maxSpread;
	[SerializeField] private float fireRate;

	#endregion

	#region Properties

	public WeaponType Type => type;

	public float Range => range;
	public float MaxSpread => maxSpread;
	public float FireRate => fireRate;

	#endregion

	
}