using System;
using Cinemachine;
using UnityEngine;

public class CameraMoveController : MonoBehaviour, IUpdateable
{
	[SerializeField] private Transform freeLookCameraTransform;

	private float _currentMoveDistance = 50f;
	
	private const float MinMoveDistance = 50f;
	private const float MaxMoveDistance = 100f;
	private const float MoveDistanceChangeStep = 10f;
	
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
			_currentMoveDistance += deltaTime * MoveDistanceChangeStep;
		}
		else if (_currentMoveDistance > MinMoveDistance)
		{
			_currentMoveDistance = MinMoveDistance;
		}

		_currentMoveDistance = Mathf.Clamp(_currentMoveDistance, MinMoveDistance, MaxMoveDistance);
		Debug.Log(_currentMoveDistance);
		
		freeLookCameraTransform.position = Vector3.MoveTowards(freeLookCameraTransform.position, pos, _currentMoveDistance * deltaTime);
	}
}
