using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class NavPathGizmo : Editor
{
	[DrawGizmo(GizmoType.Selected | GizmoType.NonSelected)]
	static void DrawNavPath(NavMeshAgent nav, GizmoType gizmoType)
	{
		Gizmos.color = Color.red;

		Vector3[] points = nav.path.corners;

		for(int i = 0; i < points.Length - 1; i++)
		{
			Gizmos.DrawLine(points[i], points[i+1]);
		}
	}
}
