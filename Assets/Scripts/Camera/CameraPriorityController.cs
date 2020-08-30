using Cinemachine;
using UnityEngine;

public class CameraPriorityController : MonoBehaviour
{
	private CinemachineVirtualCamera currentCamera;

	public void SetAsCurrentCamera(CinemachineVirtualCamera newCamera)
	{
		currentCamera.Priority = 0;
		currentCamera = newCamera;
		currentCamera.Priority = 100;
	}
}
