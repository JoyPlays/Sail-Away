using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomController : MonoBehaviour, IUpdateable
{
	[SerializeField] private Transform cameraTransform;
	
	[SerializeField] private CameraDataSO cameraData;

	private float _currentZoomValue = 0f;

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
		float scrollAxis = Input.GetAxis("Mouse ScrollWheel");
		
		if (Mathf.Abs(scrollAxis) > 0.001f)
		{
			_currentZoomValue += scrollAxis * cameraData.zoomStep;
			_currentZoomValue = Mathf.Clamp(_currentZoomValue, cameraData.maxZoomOutValue, cameraData.maxZoomInValue);
		}
		
		Vector3 zoomPosition = cameraTransform.forward * _currentZoomValue;
		Vector3 newPosition = Vector3.MoveTowards(cameraTransform.localPosition, zoomPosition, cameraData.zoomDelta * deltaTime);
		
		cameraTransform.localPosition = newPosition;
	}
}
