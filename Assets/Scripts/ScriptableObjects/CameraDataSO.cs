using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraData", menuName = "ScriptableObjects/Camera/CameraData")]
public class CameraDataSO : ScriptableObject
{
	public float minMoveDistanceSpeed = 50f;
	public float maxMoveDistanceSpeed = 100f;
	public float moveDistanceSpeedChangeStep = 10f;

	public CameraBounds cameraBounds;
}


