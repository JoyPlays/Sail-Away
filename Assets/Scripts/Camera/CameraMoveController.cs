﻿using System;
using System.ComponentModel;
using Cinemachine;
using Unity.Collections;
using UnityEngine;

public class CameraMoveController : MonoBehaviour, IUpdateable
{
	[SerializeField] private Transform freeLookCameraTransform;
	[SerializeField] private CameraDataSO cameraData;

	private float _currentMoveDistance = 50f;
	
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
		float horizontalAxis = Input.GetAxis("Horizontal");
		float verticalAxis = Input.GetAxis("Vertical");
		Vector3 pos = freeLookCameraTransform.position;
		
		if (Math.Abs(horizontalAxis) > 0.001f || Math.Abs(verticalAxis) > 0.001f)
		{
			pos.x += horizontalAxis;
			pos.z += verticalAxis;
			_currentMoveDistance += deltaTime * cameraData.moveDistanceSpeedChangeStep;
		}
		else if (_currentMoveDistance > cameraData.minMoveDistanceSpeed)
		{
			_currentMoveDistance = cameraData.minMoveDistanceSpeed;
		}

		_currentMoveDistance = Mathf.Clamp(_currentMoveDistance, cameraData.minMoveDistanceSpeed, cameraData.maxMoveDistanceSpeed);
		
		Vector3 newPosition = Vector3.MoveTowards(freeLookCameraTransform.position, pos, _currentMoveDistance * deltaTime);
		newPosition.x = Mathf.Clamp(newPosition.x, cameraData.cameraBounds.left, cameraData.cameraBounds.right);
		newPosition.z = Mathf.Clamp(newPosition.z, cameraData.cameraBounds.bottom, cameraData.cameraBounds.top);

		freeLookCameraTransform.position = newPosition;
	}
}
