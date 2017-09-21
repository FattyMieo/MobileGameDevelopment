using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinchZoomController : MonoBehaviour
{
	public bool kbmMode;
	float initDistance;
	float distanceDelta;
	Vector3 altFingerPos;
	Vector3 lastMousePos;

	// Use this for initialization
	void Start ()
	{
		lastMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(kbmMode)
			KPMPinch();
		else
			TouchPinch();
	}

	void TouchPinch()
	{
		if(Input.touchCount == 2)
		{
			//Get initial distance between two fingers when the second finger first touches the device screen
			if(Input.GetTouch(1).phase == TouchPhase.Began)
			{
				initDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
			}

			if(Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
			{
				float newDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);
				distanceDelta = newDistance - initDistance;

				this.transform.Translate(Vector3.forward * distanceDelta * Time.deltaTime, Space.Self);
			}
		}
		else
		{
			distanceDelta = Mathf.MoveTowards(distanceDelta, 0f, Time.deltaTime);
		}
	}

	void KPMPinch()
	{
		//Simulate first touch
		if(Input.GetKeyDown(KeyCode.LeftAlt))
		{
			altFingerPos = Input.mousePosition;
		}

		if(Input.GetMouseButtonDown(0) && Input.GetKey(KeyCode.LeftAlt))
		{
			initDistance = Vector3.Distance(Input.mousePosition, altFingerPos);
		}

		//If both fingers are pressed down
		if(Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(0))
		{
			Vector3 mousePosDelta = Input.mousePosition - lastMousePos;

			if(mousePosDelta.magnitude > 0f)
			{
				float newDistance = Vector3.Distance(Input.mousePosition, altFingerPos);
				distanceDelta = newDistance - initDistance;

				this.transform.Translate(Vector3.forward * distanceDelta * Time.deltaTime, Space.Self);
			}
		}
		else
		{
			distanceDelta = Mathf.MoveTowards(distanceDelta, 0f, Time.deltaTime);
		}

		lastMousePos = Input.mousePosition;
	}
}
