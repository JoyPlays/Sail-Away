using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName  = "Ship Data", menuName = "Ships/Ship Data")]
public class ShipData : ScriptableObject
{
	[SerializeField] private ShipFacade shipPrefab;
	[SerializeField] private ShipAttributes shipAttributes;
}
