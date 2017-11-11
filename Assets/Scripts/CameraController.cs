using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	private Transform pointer;

	// Use this for initialization
	void Start ()
	{
		pointer = this.transform.parent;
	}

	private Vector3 originPos = Vector3.zero;

	private Vector2 anchorPos = Vector2.zero;
	private Vector2 movedPos = Vector2.zero;

	public float panSpeed;
	public float panDistance;

	// Update is called once per frame
	void Update ()
	{
		if(Input.touchCount == 2)
		{
			Touch t0 = Input.GetTouch(0);
			Touch t1 = Input.GetTouch(1);
			Vector2 avgPos = (t0.position + t1.position) / 2.0f;
			if(t0.phase == TouchPhase.Began || t1.phase == TouchPhase.Began)
			{
				anchorPos = avgPos;
				originPos = pointer.position;
			}
			if(t0.phase == TouchPhase.Moved || t1.phase == TouchPhase.Moved)
			{
				movedPos = avgPos;
			}
//			switch(t.phase)
//			{
//				case TouchPhase.Began:
//					anchorPos = t.position;
//					originPos = pointer.position;
//					break;
//				case TouchPhase.Moved:
//					movedPos = t.position;
//					break;
//			}
		}
		else
		{
			if(Input.GetMouseButtonDown(0))
			{
				anchorPos = Input.mousePosition;
				originPos = pointer.position;
			}
			else if(Input.GetMouseButton(0))
			{
				movedPos = Input.mousePosition;
			}
		}

		Vector2 dir = movedPos - anchorPos;
		dir *= panDistance;
		pointer.position = Vector3.Lerp(pointer.position, originPos + new Vector3(dir.x, 0.0f, dir.y), Time.deltaTime * panSpeed);
	}
}
