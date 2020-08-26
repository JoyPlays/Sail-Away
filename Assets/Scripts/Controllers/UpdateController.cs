using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
	private List<IUpdateable> _updateableObjects = new List<IUpdateable>();
	
	private static UpdateController _instance;
	public static UpdateController Instance
	{
		get => _instance;
		private set => _instance = value;
	}

	private void Awake()
	{
		Instance = this;
	}

	public void RegisterUpdateableObject(IUpdateable obj)
	{
		if (!_updateableObjects.Contains(obj))
		{
			_updateableObjects.Add(obj);
		}
	}

	public void DeregisterUpdateableObject(IUpdateable obj)
	{
		if (_updateableObjects.Contains(obj))
		{
			_updateableObjects.Remove(obj);
		}
	}

	void Update()
	{
		float dt = Time.deltaTime;
		for (int i = 0; i < _updateableObjects.Count; ++i)
		{
			_updateableObjects[i].OnUpdate(dt);
		}
	}
}
