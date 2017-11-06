//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Test : MonoBehaviour {
//
//	//Caching
//	public Transform _transform;
//	public Renderer _rend;
//
//	// Use this for initialization
//	void Start ()
//	{
//		//Caching
//		this._transform == this.GetComponent<Transform>();
//		this._rend == this.GetComponent<MeshRenderer>();
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if(posX != 1f) //Call function when necessary only
//		{
//			RandomFunction();
//		}
//
//		List<GameObject> stuff = new List<GameObject>();
//		stuff.Clear(); // Clear collections
//
//		Debug.Log(string.Format("??? {0}", gameObject.name)); //Boxing, don't do this
//	}
//
//	IEnumerator Sequence()
//	{
//		float delay = 1f;
//
//		yield return delay;
//		yield return new WaitForSeconds(1); //Don't do this
//	}
//}
