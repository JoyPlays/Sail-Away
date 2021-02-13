using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetGatherController : MonoBehaviour, IUpdateable
{
	
	#region Fields
	
	[SerializeField] private WeaponAimController aimController;
	[SerializeField] private SphereCollider sphereCollider;

	private float angle = 90f;

	#endregion
	
	#region Properties
	
	public List<Transform> targetsInRange { get; private set; } = new List<Transform>();
	public List<Transform> targetsInCone { get; private set; } = new List<Transform>();
	
	#endregion

	public void Setup(WeaponDataSO data)
	{
		sphereCollider.radius = data.Range * 2f;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!targetsInRange.Contains(other.transform))
		{
			targetsInRange.Add(other.transform);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (targetsInRange.Contains(other.transform))
		{
			targetsInRange.Remove(other.transform);
		}
		
		if (targetsInCone.Contains(other.transform))
		{
			targetsInCone.Remove(other.transform);
		}
		
		if (aimController.Target == other.transform)
		{
			aimController.Target = null;
			TryGetTargetFormList();
		}
	}

	private void Start()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.RegisterUpdateableObject(this);
		}
	}
	
	public void OnUpdate(float deltaTime)
	{
		//TODO: add something to not calculate every frame
		
		for (int i = 0; i < targetsInRange.Count; i++) 
		{
			Vector3 dir = (targetsInRange[i].position - transform.position).normalized;
			float dot = Vector3.Dot(transform.forward, dir);
			float cos = Mathf.Cos(angle * Mathf.Deg2Rad);
			if (dot > cos)
			{
				if (!targetsInCone.Contains(targetsInRange[i]))
				{
					targetsInCone.Add(targetsInRange[i]);

					if (!aimController.Target)
					{
						TryGetTargetFormList();
					}
				}
			}
			else
			{
				if (targetsInCone.Contains(targetsInRange[i]))
				{
					targetsInCone.Remove(targetsInRange[i]);
				}
				
				if (aimController.Target == targetsInRange[i])
				{
					aimController.Target = null;
					TryGetTargetFormList();
				}
			}
		}
	}

	private void OnDestroy()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.DeregisterUpdateableObject(this);
		}
	}
	
	public void TryGetTargetFormList()
	{
		targetsInCone = targetsInCone.OrderBy(x => Vector3.Distance(x.position, transform.position)).ToList();

		foreach (var target in targetsInCone)
		{
			aimController.Target = target;
			break;
		}
	}

	
}
