using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragMoveController : MonoBehaviour {

	public bool mouseMode;
	public float baseSpeed = 10.0f;
	Vector3 direction;
	float magnitude = 0.0f;
	Vector3 lastMousePos;

	// Use this for initialization
	void Start ()
	{
		lastMousePos = Input.mousePosition;
	}
	
	// Update is called once per frame
	void Update ()
	{
		//Set direction, magnotude
		if(mouseMode) MouseDrag();
		else TouchDrag();
	}

	void MouseDrag()
	{
		if(Input.GetMouseButton(0)) //Get direction when mouse is down
		{
			direction = (Input.mousePosition - lastMousePos).normalized;
			magnitude = (Input.mousePosition - lastMousePos).magnitude;
		}
		else
		{
			direction = Vector3.MoveTowards(direction, Vector3.zero, Time.deltaTime);
		}
		lastMousePos = Input.mousePosition;
	}

	void TouchDrag()
	{
		if(Input.touchCount > 0)
		{
			direction = Input.GetTouch(0).deltaPosition.normalized;
			magnitude = Input.GetTouch(0).deltaPosition.magnitude;
		}
		else
		{
			direction = Vector3.MoveTowards(direction, Vector3.zero, Time.deltaTime);
		}
	}

	//Move based on direction, magnitude and damping
	void LateUpdate()
	{
		Vector3 worldDir = new Vector3(direction.x, 0, direction.y);
		this.transform.Translate(-worldDir * Time.deltaTime * baseSpeed * magnitude, Space.World);
	}
}
