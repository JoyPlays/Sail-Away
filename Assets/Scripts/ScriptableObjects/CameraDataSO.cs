using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/Camera/CameraData")]
public class CameraDataSO : ScriptableObject
{
	[Header ("Movement")]
	public float minMoveDistanceSpeed = 50f;
	public float maxMoveDistanceSpeed = 100f;
	public float moveDistanceSpeedChangeStep = 10f;
	public CameraBounds cameraBounds;
	
	[Header ("Zoom")]
	public float maxZoomOutValue = -50;
	public float maxZoomInValue = 80f;
	public float zoomStep = 25f;
	public float zoomDelta = 50f;

	
}


