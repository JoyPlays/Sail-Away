using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
	[Serializable]
	public class Pool
	{
		public string tag;
		public GameObject prefab;
		public int size;
	}
	
	public static ObjectPooler Instance { get; private set; }

	[SerializeField] private List<Pool> pools;
	
	
	public Dictionary<string, Queue<GameObject>> PoolDictionary { get; set; }

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		PoolDictionary = new Dictionary<string, Queue<GameObject>>();

		foreach (var pool in pools)
		{
			Queue<GameObject> objectPool = new Queue<GameObject>();

			for (int i = 0; i < pool.size; i++)
			{
				GameObject obj = Instantiate(pool.prefab, transform);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}
			
			PoolDictionary.Add(pool.tag, objectPool);
		}
	}

	public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
	{
		if (!PoolDictionary.ContainsKey(tag))
		{
			Debug.LogError($"Pool dictionary doesn't contain key: {tag}");
			return null;
		}
		
		GameObject objectToSpawn = PoolDictionary[tag].Dequeue();
		
		objectToSpawn.SetActive(true);
		objectToSpawn.transform.position = position;
		objectToSpawn.transform.rotation = rotation;

		IPooledObject pooledObj = objectToSpawn.GetComponent<IPooledObject>();

		if (pooledObj != null)
		{
			pooledObj.OnObjectSpawn();
		}
		
		PoolDictionary[tag].Enqueue(objectToSpawn);

		return objectToSpawn;
	}
}
