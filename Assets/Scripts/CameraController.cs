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
		#if UNITY_ANDROID && !UNITY_EDITOR
		Debug.Log("Using Android");
		#elif UNITY_EDITOR
		Debug.Log("Using Editor");
		#endif
	}

	private Vector3 originPos = Vector3.zero;

	private Vector2 anchorPos = Vector2.zero;
	private Vector2 movedPos = Vector2.zero;

	private bool hasBegan = false;
	private bool isPressedDown0 = false;
	private bool isPressedDown1 = false;

	//Debug UI
	public RectTransform anchorTrans;
	public RectTransform movedTrans;
	public RectTransform touch0;
	public RectTransform touch1;

	public float panSpeed;
	public float panDistance;

	// Update is called once per frame
	void Update ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(Input.touchCount == 2)
		{
			Touch t0 = Input.GetTouch(0);
			Touch t1 = Input.GetTouch(1);
			touch0.position = t0.position;
			touch1.position = t1.position;
			Vector2 avgPos = Vector3.Lerp(t0.position, t1.position, 0.5f);
			
			if(t0.phase == TouchPhase.Began) isPressedDown0 = true;
			if(t1.phase == TouchPhase.Began) isPressedDown1 = true;
			
			if(!hasBegan)
			{
				if(isPressedDown0 && isPressedDown1)
				{
					anchorPos = avgPos;
					movedPos = avgPos;
					originPos = pointer.position;
					hasBegan = true;
				}
			}
			if(t0.phase == TouchPhase.Moved || t1.phase == TouchPhase.Moved)
			{
				movedPos = avgPos;
			}

			if(t0.phase == TouchPhase.Canceled || t0.phase == TouchPhase.Ended) { isPressedDown0 = false; hasBegan = false; }
			if(t1.phase == TouchPhase.Canceled || t1.phase == TouchPhase.Ended) { isPressedDown1 = false; hasBegan = false; }
		}
		#elif UNITY_EDITOR
		Vector2 avgPos = Input.mousePosition;

		if(Input.GetMouseButtonDown(0))
		{
			isPressedDown0 = true;
		}
		if(Input.GetMouseButtonDown(1))
		{
			isPressedDown1 = true;
		}

		if(!hasBegan)
		{
			if(isPressedDown0 && isPressedDown1)
			{
				anchorPos = avgPos;
				movedPos = avgPos;
				originPos = pointer.position;
				hasBegan = true;
			}
		}
		if(Input.GetMouseButton(0) || Input.GetMouseButton(1))
		{
			movedPos = avgPos;
		}

		if(Input.GetMouseButtonUp(0))
		{
			isPressedDown0 = false;
			hasBegan = false;
		}
		if(Input.GetMouseButtonUp(1))
		{
			isPressedDown1 = false;
			hasBegan = false;
		}
		#endif

		Vector2 dir = anchorPos - movedPos;
		dir *= panDistance;
		Vector3 dir3 = new Vector3(dir.x, 0.0f, dir.y);
		pointer.position = Vector3.Lerp(pointer.position, originPos + dir3, Time.deltaTime * panSpeed);

		//Debug UI
		anchorTrans.position = anchorPos;
		movedTrans.position = movedPos;
	}
}
