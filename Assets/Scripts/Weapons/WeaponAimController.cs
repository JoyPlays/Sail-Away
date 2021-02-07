﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAimController : MonoBehaviour, IAimable
{
	[SerializeField] private AnimationCurve barrelAimCurve = AnimationCurve.Linear(0f,0f, 1f, 1f);
	
	[SerializeField] private Vector3 minBarrelRot = new Vector3(0f, 0f, 0f);
	[SerializeField] private Vector3 maxBarrelRot = new Vector3(-30f, 0f, 0f);
	
	[SerializeField] private float range = 10;
	
	[SerializeField] private Transform bodyTransform;
	[SerializeField] private Transform barrelTransform;
	
	
	[SerializeField] private Transform target;

	public Transform Target => target;

	private float distanceToTarget;

	private void Update()
	{
		if (Target)
		{
			distanceToTarget = Vector3.Distance(bodyTransform.position, Target.position);
			if (distanceToTarget <= range)
			{
				AimAtTarget();
			}
		}
	}

	public void AimAtTarget()
	{
		Vector3 dirToTarget = (Target.position - bodyTransform.position).normalized;
		dirToTarget.y = 0f;
		Quaternion rotationToTarget = Quaternion.LookRotation(dirToTarget, Vector3.up);
		bodyTransform.rotation = Quaternion.RotateTowards(bodyTransform.rotation, rotationToTarget, 30f * Time.deltaTime);
		bodyTransform.localRotation = ClampRotationAroundYAxis(bodyTransform.localRotation);

		float t = Mathf.InverseLerp(2f, range, distanceToTarget);

		Vector3 barrelRotation = Vector3.Lerp(minBarrelRot, maxBarrelRot, barrelAimCurve.Evaluate(t));
		barrelTransform.localRotation = Quaternion.RotateTowards(barrelTransform.localRotation, Quaternion.Euler(barrelRotation), 10f * Time.deltaTime);
	}
	
	void OnDrawGizmos()
	{
		Gizmos.color = new Color(0f, 0.8f, 0.2f, 0.9f);
		Gizmos.DrawWireSphere(transform.position, range);
	}
	
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
