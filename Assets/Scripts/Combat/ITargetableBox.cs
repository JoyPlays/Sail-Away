using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITargetableBox : ITargetable
{
	float LeftBound { get; }
	float RightBound { get; }
	float ForwardBound { get; }
	float BackBound { get; }
}
