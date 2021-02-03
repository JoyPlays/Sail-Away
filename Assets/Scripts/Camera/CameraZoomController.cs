using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraZoomController : MonoBehaviour, IUpdateable
{
	[SerializeField] private Transform cameraTransform;
	
	[SerializeField] private CameraDataSO cameraData;

	[SerializeField] private CinemachineVirtualCamera currentVirtualCamera;

	private float _currentZoomValue = 50f;

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
		
		#region Ortographic Zoom

		if (Mathf.Abs(scrollAxis) > 0.001f)
		{
			_currentZoomValue -= scrollAxis * cameraData.zoomStep;
			_currentZoomValue = Mathf.Clamp(_currentZoomValue, cameraData.maxZoomInValue, cameraData.maxZoomOutValue);
		}
		
		float currentSize = currentVirtualCamera.m_Lens.OrthographicSize;
		float newSize = Mathf.MoveTowards(currentSize, _currentZoomValue, cameraData.zoomDelta * deltaTime);
		currentVirtualCamera.m_Lens.OrthographicSize = newSize;

		#endregion

		#region Perspective Zoom

		// if (Mathf.Abs(scrollAxis) > 0.001f)
		// {
		// 	_currentZoomValue += scrollAxis * cameraData.zoomStep;
		// 	_currentZoomValue = Mathf.Clamp(_currentZoomValue, cameraData.maxZoomOutValue, cameraData.maxZoomInValue);
		// }
		
		// Vector3 zoomPosition = cameraTransform.forward * _currentZoomValue;
		// Vector3 newPosition = Vector3.MoveTowards(cameraTransform.localPosition, zoomPosition, cameraData.zoomDelta * deltaTime);
		// cameraTransform.localPosition = newPosition;

		#endregion
	}
}
