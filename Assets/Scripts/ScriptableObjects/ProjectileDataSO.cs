using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileData", menuName = "ScriptableObjects/Projectiles", order = 1)]
public class ProjectileDataSO : ScriptableObject
{
	#region Fields

	[SerializeField] private float damage;
	//

	#endregion

	#region Properties

	public float Damage => damage;

	#endregion
}
