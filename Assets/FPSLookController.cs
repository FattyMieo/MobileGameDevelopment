using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSLookController : MonoBehaviour, IDragHandler, IEndDragHandler
{
	Vector3 initPos;
	Vector3 direction;
	public Transform yAxis;
	public PlayerScript player;
	public Transform xAxis1;
	public Transform xAxis2;
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
		if(direction.normalized.y > 0)
		{
			if(player.curVAngle - Time.deltaTime * player.vTurnSpeed >= player.minVAngle)
			{
				player.curVAngle -= Time.deltaTime * player.vTurnSpeed;
				player.camFPS.transform.Rotate(-Time.deltaTime * player.vTurnSpeed, 0, 0);
				player.camTPS.transform.Rotate(-Time.deltaTime * player.vTurnSpeed, 0, 0);
			}
		}
		if(direction.normalized.y < 0)
		{
			if(player.curVAngle + Time.deltaTime * player.vTurnSpeed <= player.maxVAngle)
			{
				player.curVAngle += Time.deltaTime * player.vTurnSpeed;
				player.camFPS.transform.Rotate(Time.deltaTime * player.vTurnSpeed, 0, 0);
				player.camTPS.transform.Rotate(Time.deltaTime * player.vTurnSpeed, 0, 0);
			}
		}
	}
}
