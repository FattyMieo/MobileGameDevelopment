using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[Header("Transforms")]
	public Transform pointer;
	public Transform minZoom;
	public Transform maxZoom;

	//Initial pointer's Position
	private Vector3 originPos = Vector3.zero;

	//Screen Positions
	private Vector2 anchorPos = Vector2.zero;
	private Vector2 movedPos = Vector2.zero;
	private Vector2 atouch0Pos = Vector2.zero;
	private Vector2 touch0Pos = Vector2.zero;
	private Vector2 atouch1Pos = Vector2.zero;
	private Vector2 touch1Pos = Vector2.zero;
	private float aZoomValue = 0.0f;
	private float aZoomDistance = 0.0f;

	//Booleans for checking two touches
	private bool isStationary = false;
	private bool hasBegan = false;
	private bool isPressedDown0 = false;
	private bool isPressedDown1 = false;
	private bool isPressedDownCtrl = false;

	[Header("Camera Pan")]
	public float panSpeed;
	public float panDistance;

	[Header("Camera Zoom")]
	public float zoomSpeed;
	public float zoomStep;
	[Range(0.0f, 1.0f)] public float zoomValue;

	[Header("Debug UI")]
	public bool debugMode;
	public GameObject debugCanvas;
	public RectTransform anchorTrans;
	public RectTransform movedTrans;
	public RectTransform atouch0;
	public RectTransform atouch1;
	public RectTransform touch0;
	public RectTransform touch1;

	void Start()
	{
//		#if UNITY_EDITOR
//		touch0.gameObject.SetActive(false);
//		touch1.gameObject.SetActive(false);
//		#endif
		aZoomValue = zoomValue;
	}

	// Update is called once per frame
	void Update ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		PanCameraAndroid();
		#elif UNITY_EDITOR
		PanCameraEditor();
		#endif

		MoveCameraPointer();
		ZoomCamera();

		//Debug UI
		debugCanvas.SetActive(debugMode);
		if(debugMode)
		{
			anchorTrans.position = anchorPos;
			movedTrans.position = movedPos;
			touch0.position = touch0Pos;
			touch1.position = touch1Pos;
			atouch0.position = atouch0Pos;
			atouch1.position = atouch1Pos;
		}
	}

	#if UNITY_ANDROID && !UNITY_EDITOR
	void PanCameraAndroid()
	{
		if(Input.touchCount == 2)
		{
			Touch t0 = Input.GetTouch(0);
			Touch t1 = Input.GetTouch(1);
			touch0Pos = t0.position;
			touch1Pos = t1.position;
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
					atouch0Pos = touch0Pos;
					atouch1Pos = touch1Pos;
					aZoomValue = zoomValue;
					aZoomDistance = Vector2.Distance(atouch0Pos, atouch1Pos);
					hasBegan = true;
					isStationary = false;
				}
			}
			if(t0.phase == TouchPhase.Stationary && t1.phase == TouchPhase.Stationary)
			{
				isStationary = true;
			}
			if(t0.phase == TouchPhase.Moved || t1.phase == TouchPhase.Moved)
			{
				if(isStationary)
				{
					anchorPos = avgPos;
					movedPos = avgPos;
					originPos = pointer.position;
					atouch0Pos = touch0Pos;
					atouch1Pos = touch1Pos;
					aZoomValue = zoomValue;
					aZoomDistance = Vector2.Distance(atouch0Pos, atouch1Pos);
					isStationary = false;
				}
				movedPos = avgPos;

				float mZoomDistance = Vector2.Distance(touch0Pos, touch1Pos);
				zoomValue = Mathf.Clamp01(aZoomValue + ((mZoomDistance - aZoomDistance) * zoomStep));
			}

			if(t0.phase == TouchPhase.Canceled || t0.phase == TouchPhase.Ended) { isPressedDown0 = false; hasBegan = false; isStationary = false;}
			if(t1.phase == TouchPhase.Canceled || t1.phase == TouchPhase.Ended) { isPressedDown1 = false; hasBegan = false; isStationary = false;}
		}
	}
	#endif

	#if UNITY_EDITOR
	void PanCameraEditor()
	{
		isPressedDownCtrl = Input.GetKey(KeyCode.LeftControl);

		if(!isPressedDownCtrl) touch0Pos = Input.mousePosition;
		touch1Pos = Input.mousePosition;
		Vector2 avgPos = Vector3.Lerp(touch0Pos, touch1Pos, 0.5f);

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
				atouch0Pos = touch0Pos;
				atouch1Pos = touch1Pos;
				aZoomValue = zoomValue;
				aZoomDistance = Vector2.Distance(atouch0Pos, atouch1Pos);
				hasBegan = true;
			}
		}
		if(Input.GetMouseButton(0) && Input.GetMouseButton(1))
		{
			movedPos = avgPos;

			float mZoomDistance = Vector2.Distance(touch0Pos, touch1Pos);
			zoomValue = Mathf.Clamp01(aZoomValue + ((mZoomDistance - aZoomDistance) * zoomStep));
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
	}
	#endif

	void MoveCameraPointer()
	{
		Vector3 dir = (anchorPos - movedPos).ToXZ() * panDistance;
		pointer.position = Vector3.Lerp(pointer.position, originPos + dir, Time.deltaTime * panSpeed);
	}

	void ZoomCamera()
	{
		Vector3 targetPos = Vector3.Lerp(minZoom.position, maxZoom.position, zoomValue);
		this.transform.position = Vector3.Lerp(this.transform.position, targetPos, Time.deltaTime * zoomSpeed);
		Quaternion targetRot = Quaternion.Lerp(minZoom.rotation, maxZoom.rotation, zoomValue);
		this.transform.rotation = Quaternion.Lerp(this.transform.rotation, targetRot, Time.deltaTime * zoomSpeed);
	}
}
