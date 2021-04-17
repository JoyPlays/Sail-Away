using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Unistoty.SOVariables;
using UnityEngine;

public class PlayerFacade : MonoBehaviour, IShipLoader
{
	[SerializeField] private Transform shipContainer;

	[SerializeField] private AIPath aiPath;
	[SerializeField] private IntVariable viewRange;

	public int CurrentHitPoints { get; private set; }	//Temp
	public int MaxHitPoints { get; private set; }		//Temp
	public int CargoHold { get; private set; }			//Temp
	public int ViewRange
	{
		get => viewRange.Value;
		private set => viewRange.Value = value;

	}

	private void Start()
	{
		DebugShipOnStart();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.KeypadPlus))
		{
			NextDebugShip();
		}
	}

	public void LoadShip(ShipData shipData)
	{
		if (!shipData)
		{
			return;
		}

		if (shipContainer.childCount > 0)
		{
			Destroy(shipContainer.GetChild(0).gameObject);
		}
		
		Instantiate(shipData.ShipPrefab, shipContainer.position, shipContainer.rotation, shipContainer);
		SetShipAttributes(shipData.ShipAttributes);
	}

	private void SetShipAttributes(ShipAttributes attributes)
	{
		aiPath.maxSpeed = attributes.Speed;
		aiPath.rotationSpeed = attributes.TurnRate;

		MaxHitPoints = CurrentHitPoints = attributes.HitPoints;
		CargoHold = attributes.CargoHold;
		ViewRange = attributes.ViewRange;
	}
	
	#region Debug
	
	[SerializeField] private List<ShipData> debugShipList = new List<ShipData>();
	private int debugShipListIndex = 0;

	private void DebugShipOnStart()
	{
		for (int i = debugShipList.Count - 1; i >= 0; i--)
		{
			if (debugShipList[i] == null)
			{
				debugShipList.RemoveAt(i);
			}
		}
		
		if (debugShipList.Count <= 0)
		{
			return;
		}
		
		LoadShip(debugShipList[0]);
	}

	private void NextDebugShip()
	{
		if (debugShipList.Count <= 0)
		{
			return;
		}

		if (debugShipListIndex < debugShipList.Count - 1)
		{
			debugShipListIndex++;
		}
		else
		{
			debugShipListIndex = 0;
		}
			
		LoadShip(debugShipList[debugShipListIndex]);
	}

	#endregion
}
