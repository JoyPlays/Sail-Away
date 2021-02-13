using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/Weapons", order = 1)]
public class WeaponDataSO : ScriptableObject
{
	#region Fields

	[SerializeField] private WeaponType type;
	[SerializeField] private float range;
	[SerializeField] private float accuracy;
	[SerializeField] private float projectileSpeed;

	#endregion

	#region Properties

	public WeaponType Type => type;

	public float Range => range;
	
	public float Accuracy => accuracy;

	public float ProjectileSpeed => projectileSpeed;

	#endregion

	
}