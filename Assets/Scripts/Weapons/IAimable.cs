﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAimable
{
	Transform Target { get; }

	void AimAtTarget();
}