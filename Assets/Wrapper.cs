using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Wrapper : MonoBehaviour {

	public Vortex vortex;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 screenSpacePoint = Camera.main.WorldToViewportPoint(this.transform.position);
		vortex.center = new Vector2(screenSpacePoint.x, screenSpacePoint.y);
	}
}
