using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFadeable
{
	List<Material> FadeMaterials { get; set; }
	
	bool GraphicsActive { get; set; }

	void SetFadeAmount();

	void SetGraphicsActive(bool active);
}
