using System;
using System.Collections;
using System.Collections.Generic;
using Unistoty.SOVariables;
using UnityEngine;

public class ViewRangeObjectActivator : MonoBehaviour, IUpdateable
{
	[SerializeField] private IntVariable playerViewRange;
	[SerializeField] private Transform playerTransform;
	
	private int frames = 0;
	
	private List<IViewRangeItem> viewRangeItems = new List<IViewRangeItem>();
	
	#region Singletone
	
	private static ViewRangeObjectActivator _instance;
	public static ViewRangeObjectActivator Instance
	{
		get => _instance;
		private set => _instance = value;
	}
	
	#endregion

	private void Awake()
	{
		Instance = this;
	}
	
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

	public void RegisterViewRangeItem(IViewRangeItem obj)
	{
		if (!viewRangeItems.Contains(obj))
		{
			viewRangeItems.Add(obj);
		}
	}

	public void DeregisterViewRangeItem(IViewRangeItem obj)
	{
		if (viewRangeItems.Contains(obj))
		{
			viewRangeItems.Remove(obj);
		}
	}

	public void OnUpdate(float deltaTime)
	{
		if (frames % 2 == 0)
		{
			for (int i = 0; i < viewRangeItems.Count; i++)
			{
				if (Vector3.Distance(viewRangeItems[i].Transform.position, playerTransform.position) <= playerViewRange.Value)
				{
					viewRangeItems[i].SetGraphicsActive(true);
				}
				else
				{
					viewRangeItems[i].SetGraphicsActive(false);
				}
			}
		}

		frames++;
	}
}
