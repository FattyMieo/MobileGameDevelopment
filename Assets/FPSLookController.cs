using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSLookController : MonoBehaviour, IDragHandler, IEndDragHandler
{
	Vector3 initPos;
	Vector3 direction;
	public Transform yAxis;
	public Transform xAxis;
	public float clampedX;
	public Transform targetXRotation;
	public float rotSpeed = 15.0f;

	// Use this for initialization
	void Start ()
	{
		initPos = this.transform.position; //Set Initial Position
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position; //Make knob follow the drag
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.position = initPos; //Reset position when drag stops
		direction = Vector3.zero; //Reset direction to stop rotating
	}

	// Update is called once per frame
	void Update ()
	{
		direction = this.transform.position - initPos; //Get directional movement
	}

	void LateUpdate()
	{
		//Rotate Character in Y Axis
		yAxis.Rotate(Vector3.up * direction.normalized.x * Time.deltaTime * rotSpeed);

		//Rotate camera in X axis
		clampedX = Mathf.Clamp(direction.y, -30.0f, 30.0f); //Clamp float x value
		targetXRotation.localEulerAngles = new Vector3(clampedX, 0.0f, 0.0f) * -1.0f; //Setup target rotation, * -1.0f to cancel our inversion
		xAxis.rotation = Quaternion.Lerp(xAxis.rotation, targetXRotation.rotation, Time.deltaTime * rotSpeed); //Rotate towards target rotation
	}
}
