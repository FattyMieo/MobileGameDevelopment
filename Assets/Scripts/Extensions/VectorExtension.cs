using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
	public static Vector3 ToXZ(this Vector2 vec2)
	{
		return new Vector3(vec2.x, 0.0f, vec2.y);
	}
}
