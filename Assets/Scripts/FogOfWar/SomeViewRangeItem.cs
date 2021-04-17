using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SomeViewRangeItem : MonoBehaviour, IViewRangeItem
{
	[SerializeField] private GameObject graphicsObject;

	private bool isActive = true;
	private Coroutine fadeCoroutine = null;
	
	private List<Renderer> renderers = new List<Renderer>();
	private List<Material> materials = new List<Material>();
	private List<Material> sharedMaterials = new List<Material>();
	
	public GameObject GraphicsObject => graphicsObject;
	public Transform Transform => transform;

	private void Awake()
	{
		renderers = GetComponentsInChildren<Renderer>().ToList();

		foreach (var renderer in renderers)
		{
			sharedMaterials.Add(renderer.sharedMaterial);
			materials.Add(renderer.material);
		}
	}

	private void Start()
	{
		if (ViewRangeObjectActivator.Instance)
		{
			ViewRangeObjectActivator.Instance.RegisterViewRangeItem(this);
		}
	}
	
	private void OnDestroy()
	{
		if (ViewRangeObjectActivator.Instance)
		{
			ViewRangeObjectActivator.Instance.DeregisterViewRangeItem(this);
		}
	}

	public void SetGraphicsActive(bool active)
	{
		if (isActive == active)
		{
			return;
		}

		isActive = active;
		//ShowHide sequence here

		if (active)
		{
			ShowGraphics();
		}
		else
		{
			HideGraphics();
		}
	}

	private void ShowGraphics()
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		
		fadeCoroutine = StartCoroutine(FadeSequence());
	}

	private void HideGraphics()
	{
		if (fadeCoroutine != null)
		{
			StopCoroutine(fadeCoroutine);
		}
		
		fadeCoroutine = StartCoroutine(FadeSequence());
	}

	private IEnumerator FadeSequence()
	{
		if (isActive)
		{
			GraphicsObject.SetActive(isActive);
		}
		
		float startAlpha = materials[0].color.a;
		float endAlpha = isActive ? 1f : 0f;
		
		const float FadeTime = 0.5f;

		float t = 0;

		while (t < 1)
		{
			t += Time.deltaTime / FadeTime;

			foreach (var material in materials)
			{
				Color newColor = material.GetColor(ShaderHash.COLOR);
				newColor.a = Mathf.Lerp(startAlpha, endAlpha, t);
				material.SetColor(ShaderHash.COLOR, newColor);
			}

			yield return null;
		}

		if (!isActive)
		{
			GraphicsObject.SetActive(isActive);
		}
		
		fadeCoroutine = null;
	}
}
