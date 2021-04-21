using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour, IUpdateable
{
	[SerializeField] private Camera gameCamera;
	[SerializeField] private Transform playerMovePoint;
	[SerializeField] private LayerMask rayMask;

	public bool Enabled => enabled;

	private void Start()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.RegisterUpdateableObject(this);
		}
	}
	
	private void OnDestroy()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.DeregisterUpdateableObject(this);
		}
	}
	
	public void OnUpdate(float deltaTime)
	{
		if (!enabled)
		{
			return;
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out RaycastHit hit, 1000, rayMask))
			{
				playerMovePoint.position = hit.point;
			}
		}
	}
}
