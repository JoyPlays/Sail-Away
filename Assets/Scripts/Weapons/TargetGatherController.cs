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

	private float angle = 90f; // Need to think about this one, is it weapon or ship slot related;

	#endregion
	
	#region Properties
	
	public List<ITargetable> targetsInRange { get; private set; } = new List<ITargetable>();
	public List<ITargetable> targetsInCone { get; private set; } = new List<ITargetable>();
	
	#endregion

	public void Setup(WeaponDataSO data)
	{
		sphereCollider.radius = data.Range * 2f;
	}

	private void OnTriggerEnter(Collider other)
	{
		ITargetable targetable = null;
		if (other.attachedRigidbody)
		{
			targetable = other.attachedRigidbody.GetComponentInChildren<ITargetable>();
		}
		else
		{
			targetable = other.GetComponentInChildren<ITargetable>();
		}

		if (targetable != null)
		{
			AddTargetToList(targetable);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		ITargetable targetable = null;
		if (other.attachedRigidbody)
		{
			targetable = other.attachedRigidbody.GetComponentInChildren<ITargetable>();
		}
		else
		{
			targetable = other.GetComponentInChildren<ITargetable>();
		}
		
		if (targetable != null)
		{
			RemoveTargetFromList(targetable);
		}
	}

	private void AddTargetToList(ITargetable targetable)
	{
		if (!targetsInRange.Contains(targetable))
		{
			targetsInRange.Add(targetable);
		}
	}

	private void RemoveTargetFromList(ITargetable targetable)
	{
		if (targetsInRange.Contains(targetable))
		{
			targetsInRange.Remove(targetable);
		}
		
		if (targetsInCone.Contains(targetable))
		{
			targetsInCone.Remove(targetable);
		}
		
		if (aimController.Target == targetable)
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
			Vector3 dir = (targetsInRange[i].Transform.position - transform.position).normalized;
			float dot = Vector3.Dot(transform.forward, dir);
			float cos = Mathf.Cos(angle / 2f * Mathf.Deg2Rad);
			if (dot > cos)
			{
				if (!targetsInCone.Contains(targetsInRange[i]))
				{
					targetsInCone.Add(targetsInRange[i]);

					if (aimController.Target == null)
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
		targetsInCone = targetsInCone.OrderBy(x => Vector3.Distance(x.Transform.position, transform.position)).ToList();

		foreach (var target in targetsInCone)
		{
			aimController.Target = target;
			break;
		}
	}

	
}
