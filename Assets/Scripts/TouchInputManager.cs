using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
	private TouchInputManager mInstance;
	public TouchInputManager instance
	{
		get { return mInstance; }
	}

	// Use this for initialization
	void Awake ()
	{
		if(!mInstance)
		{
			mInstance = this;
		}
		else if(mInstance != this)
		{
			Destroy(this.gameObject);
		}

		DontDestroyOnLoad(this.gameObject);
	}
}
