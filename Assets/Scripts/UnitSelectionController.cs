using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelectionController : MonoBehaviour
{
	public List<UnitScript> selectedUnits = new List<UnitScript>();

	private Vector2 touch0Pos;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		#if UNITY_ANDROID && !UNITY_EDITOR
		if(Input.touchCount == 1)
		{
			Touch t0 = Input.GetTouch(0);
			touch0Pos = t0.position;

			if(t0.phase == TouchPhase.Began)
			{
				Ray ray = Camera.main.ScreenPointToRay(touch0Pos);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 300))
				{
					if(hit.collider.tag == "Unit")
					{
						UnitScript unit = hit.collider.GetComponent<UnitScript>();
						if(!unit.isSelected)
						{
							unit.isSelected = true;
							selectedUnits.Add(unit);
						}
						else
						{
							unit.isSelected = false;
							selectedUnits.Remove(unit);
						}
					}
					else if(hit.collider.tag == "Ground")
					{
						if(selectedUnits.Count > 0)
						{
							for(int i = 0; i < selectedUnits.Count; i++)
							{
								selectedUnits[i].Move(hit.point);
							}
						}
					}
				}
			}
		}
		#elif UNITY_EDITOR
		touch0Pos = Input.mousePosition;
		if(Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
		{
			Ray ray = Camera.main.ScreenPointToRay(touch0Pos);
			RaycastHit hit;
			if(Physics.Raycast(ray, out hit, 300))
			{
				if(hit.collider.tag == "Unit")
				{
					UnitScript unit = hit.collider.GetComponent<UnitScript>();
					if(!unit.isSelected)
					{
						unit.isSelected = true;
						selectedUnits.Add(unit);
					}
					else
					{
						unit.isSelected = false;
						selectedUnits.Remove(unit);
					}
				}
				else if(hit.collider.tag == "Ground")
				{
					if(selectedUnits.Count > 0)
					{
						for(int i = 0; i < selectedUnits.Count; i++)
						{
							selectedUnits[i].Move(hit.point);
						}
					}
				}
			}
		}
		#endif
	}
}
