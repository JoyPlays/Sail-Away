using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IViewRangeItem
{
	GameObject GraphicsObject { get; }
	Transform Transform { get; }

	void SetGraphicsActive(bool active);
}
