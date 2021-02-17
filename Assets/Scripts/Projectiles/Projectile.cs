using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	private float damage;

	private void OnCollisionEnter(Collision other)
	{
		ObjectPooler.Instance.SpawnFromPool("Explosion", transform.position, Quaternion.identity);
		gameObject.SetActive(false);
	}
}
