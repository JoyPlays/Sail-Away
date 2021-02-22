using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimController : MonoBehaviour, IAimable, IUpdateable
{
	[SerializeField] private AnimationCurve barrelAimCurve = AnimationCurve.Linear(0f,0f, 1f, 1f);

	[SerializeField] private TargetGatherController targetGatherController;
	
	[SerializeField] private Vector3 minBarrelRot = new Vector3(0f, 0f, 0f);
	[SerializeField] private Vector3 maxBarrelRot = new Vector3(-30f, 0f, 0f);
	
	[SerializeField] private Transform bodyTransform;
	[SerializeField] private Transform barrelTransform;
	
	
	[SerializeField] private ITargetable target;

	private float minShootAngle = 5f;
	private float radius = 10;
	private float distanceToTarget;
	private Quaternion rotationToTarget;

	public ITargetable Target
	{
		get => target;
		set => target = value;
	}

	public bool TargetLocked
	{
		get
		{
			Vector3 direction = (Target.Transform.position - bodyTransform.position).normalized;
			float dot = Vector3.Dot(bodyTransform.forward, direction);
			float cos = Mathf.Cos(minShootAngle * Mathf.Deg2Rad);
			return dot > cos;
		}
	}



	public void Setup(WeaponDataSO data)
	{
		radius = data.Range * 2f;
		targetGatherController.Setup(data);
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
		if (Target != null)
		{
			AimAtTarget();
		}
	}

	private void OnDestroy()
	{
		if (UpdateController.Instance)
		{
			UpdateController.Instance.DeregisterUpdateableObject(this);
		}
	}

	public void AimAtTarget()
	{
		distanceToTarget = Vector3.Distance(bodyTransform.position, Target.Transform.position);
		Vector3 dirToTarget = (Target.Transform.position - bodyTransform.position).normalized;
		dirToTarget.y = 0f;
		rotationToTarget = Quaternion.LookRotation(dirToTarget, Vector3.up);
		bodyTransform.rotation = Quaternion.RotateTowards(bodyTransform.rotation, rotationToTarget, 30f * Time.deltaTime);
		//bodyTransform.localRotation = ClampRotationAroundYAxis(bodyTransform.localRotation);

		float t = Mathf.InverseLerp(2f, radius, distanceToTarget);

		Vector3 barrelRotation = Vector3.Lerp(minBarrelRot, maxBarrelRot, barrelAimCurve.Evaluate(t));
		barrelTransform.localRotation = Quaternion.RotateTowards(barrelTransform.localRotation, Quaternion.Euler(barrelRotation), 10f * Time.deltaTime);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0f, 0.8f, 0.2f, 0.9f);
		Gizmos.DrawWireSphere(transform.position, radius);
	}
	
	[Obsolete]
	Quaternion ClampRotationAroundYAxis(Quaternion q)
	{
		q.x /= q.w;
		q.y /= q.w;
		q.z /= q.w;
		q.w = 1.0f;
 
		float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.y);
		angleY = Mathf.Clamp (angleY, -30f, 30f);
		q.y = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleY);
 
		return q;
	}
}
