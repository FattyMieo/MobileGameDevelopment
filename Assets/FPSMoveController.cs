using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FPSMoveController : MonoBehaviour, IDragHandler, IEndDragHandler
{
	Vector3 initPos;
	Vector3 direction;
	public Transform characterTransform;
	bool isDragging = false;

	// Use this for initialization
	void Start ()
	{
		initPos = this.transform.position; //Set Initial Position
	}

	public void OnDrag(PointerEventData eventData)
	{
		this.transform.position = eventData.position; //Make knob follow the drag

		isDragging = true;
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		this.transform.position = initPos; //Reset position when drag stops
		direction = Vector3.zero; //Reset direction to stop rotating

		isDragging = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if(isDragging)
		{
			direction = this.transform.position - initPos; //Get directional movement
		}
		direction.Normalize();

		characterTransform.Translate(new Vector3(direction.x, 0.0f, direction.y) * Time.deltaTime * characterTransform.GetComponent<SoldierScript>().speed, Space.Self);
	}
}
