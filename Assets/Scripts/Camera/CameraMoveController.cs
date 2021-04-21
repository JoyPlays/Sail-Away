using UnityEngine;

public class CameraMoveController : MonoBehaviour, IUpdateable
{
	[SerializeField] private Transform movableTransform;
	[SerializeField] private CameraDataSO cameraData;

	private float _currentMoveDistanceDelta = 50f;

	public bool Enabled => enabled;
	
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
		Vector3 pos = movableTransform.localPosition;
		
		if (Mathf.Abs(horizontalAxis) > 0.001f || Mathf.Abs(verticalAxis) > 0.001f)
		{
			pos.x += horizontalAxis;
			pos.z += verticalAxis;
			_currentMoveDistanceDelta += deltaTime * cameraData.moveDistanceSpeedChangeStep;
		}
		else if (_currentMoveDistanceDelta > cameraData.minMoveDistanceSpeed)
		{
			_currentMoveDistanceDelta = cameraData.minMoveDistanceSpeed;
		}

		_currentMoveDistanceDelta = Mathf.Clamp(_currentMoveDistanceDelta, cameraData.minMoveDistanceSpeed, cameraData.maxMoveDistanceSpeed);
		
		Vector3 newPosition = Vector3.MoveTowards(movableTransform.localPosition, pos, _currentMoveDistanceDelta * deltaTime);
		newPosition.x = Mathf.Clamp(newPosition.x, cameraData.cameraBounds.left, cameraData.cameraBounds.right);
		newPosition.z = Mathf.Clamp(newPosition.z, cameraData.cameraBounds.bottom, cameraData.cameraBounds.top);

		movableTransform.localPosition = newPosition;
	}
}
