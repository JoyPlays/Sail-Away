using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemy : MonoBehaviour, ITargetableBox
{
	[Header("Settings")]
	[SerializeField] private float rightBound = 0.5f;
	[SerializeField] private float leftBound = 0.5f;
	[SerializeField] private float forwardBound = 1.2f;
	[SerializeField] private float backBound = 1.2f;
	
	public Transform Transform => transform;

	public float LeftBound => leftBound;
	public float RightBound => rightBound;
	public float ForwardBound => forwardBound;
	public float BackBound => backBound;

	public List<Vector3> GetBoundsList()
	{
		Vector3 position = Transform.position;
		List<Vector3> bounds = new List<Vector3> { position + Transform.right * RightBound, position - Transform.right * LeftBound, position + Transform.forward * ForwardBound, position - Transform.forward * BackBound};
		return bounds;
	}

#if UNITY_EDITOR
	private void OnDrawGizmos()
	{
		Vector3 position = Transform.position + Vector3.up;
		const float CubeSize = 0.5f;

		void Draw(Vector3 pos)
		{
			Gizmos.color = Color.green;
			Gizmos.DrawCube(pos, Vector3.one * CubeSize);
			Gizmos.color = Color.yellow;
			Gizmos.DrawLine(pos, pos + new Vector3(0f, -0.2f, 0f));
		}

		Draw(position + Transform.right * RightBound);
		Draw(position - Transform.right * LeftBound);
		Draw(position + Transform.forward * ForwardBound);
		Draw(position - Transform.forward * BackBound);
	}
#endif
}
