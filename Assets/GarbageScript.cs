using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Transform[] tArray = GameObject.FindObjectsOfType<Transform>();

//		foreach(Transform t in tArray)
//		{
//			Debug.Log(string.Format("{0}", t.name));
//		}
	}
}
