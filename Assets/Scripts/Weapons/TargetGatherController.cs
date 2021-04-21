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

	private int frames;

	#endregion
	
	#region Properties

	public bool Enabled => enabled;
	
	public List<TargetEnemy> targetsInRange { get; private set; } = new List<TargetEnemy>();
	public List<TargetEnemy> targetsInCone { get; private set; } = new List<TargetEnemy>();
	
	#endregion

	public void Setup(WeaponDataSO data)
	{
		sphereCollider.radius = data.Range * 2f;
	}

	private void OnTriggerEnter(Collider other)
	{
		TargetEnemy targetable = null;
		if (other.attachedRigidbody)
		{
			targetable = other.attachedRigidbody.GetComponentInChildren<TargetEnemy>();
		}
		else
		{
			targetable = other.GetComponentInChildren<TargetEnemy>();
		}

		if (targetable != null)
		{
			AddTargetToList(targetable);
		}
	}
	
	private void OnTriggerExit(Collider other)
	{
		TargetEnemy targetable = null;
		if (other.attachedRigidbody)
		{
			targetable = other.attachedRigidbody.GetComponentInChildren<TargetEnemy>();
		}
		else
		{
			targetable = other.GetComponentInChildren<TargetEnemy>();
		}
		
		if (targetable != null)
		{
			RemoveTargetFromList(targetable);
		}
	}

	private void AddTargetToList(TargetEnemy targetable)
	{
		if (!targetsInRange.Contains(targetable))
		{
			targetsInRange.Add(targetable);
		}
	}

	private void RemoveTargetFromList(TargetEnemy targetable)
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
		frames++;
		if (frames % 3 == 0)
		{
			for (int i = 0; i < targetsInRange.Count; i++)
			{
				
				bool inCone = AnyBoundInsideCone(targetsInRange[i]);
				
				if (inCone)
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
	}

	private bool AnyBoundInsideCone(TargetEnemy target)
	{
		List<Vector3> bounds = target.GetBoundsList();

		foreach (var bound in bounds)
		{
			Vector3 dir = (bound - transform.position).normalized;
			float dot = Vector3.Dot(transform.forward, dir);
			float cos = Mathf.Cos(angle / 2f * Mathf.Deg2Rad);
			
			if (dot > cos)
			{
				return true;
			}
		}
		
		return false;
		
		// Vector3 dir = (target.Transform.position - transform.position).normalized;
		// float dot = Vector3.Dot(transform.forward, dir);
		// float cos = Mathf.Cos(angle / 2f * Mathf.Deg2Rad);
		//return dot > cos;
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
