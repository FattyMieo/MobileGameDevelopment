using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SelectionMode
{
	ClusterMode = 0,
	FormationMode,

	Total
}

public class UnitSelectionController : MonoBehaviour, IMoveable
{
	private Camera cam;

	public List<UnitScript> selectedUnits = new List<UnitScript>();

	[Header("Status (Read Only)")]
	public bool isHolding;
	public bool isDragging;

	[Header("Settings")]
	public SelectionMode selectionMode;
	public float dragRequiredTime;
	private float dragTimer;
	public float pressDistanceThreshold;

	private Vector2 touch0Pos;

	private Vector2 initTouch0Pos;

	// Use this for initialization
	void Start ()
	{
		cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(!isDragging && isHolding)
		{
			dragTimer += Time.deltaTime;
			if(dragTimer >= dragRequiredTime)
			{
				isDragging = true;
				dragTimer = 0.0f;
			}
		}

		#if UNITY_ANDROID && !UNITY_EDITOR
		if(Input.touchCount == 1)
		{
			Touch t0 = Input.GetTouch(0);
			touch0Pos = t0.position;

			if(t0.phase == TouchPhase.Began)
			{
				isHolding = true;
				initTouch0Pos = touch0Pos;
			}
			else if(t0.phase == TouchPhase.Ended)
			{
				isHolding = false;
				if(Vector2.Distance(initTouch0Pos, touch0Pos) <= pressDistanceThreshold)
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
							Move(hit.point);
						}
					}
				}
				else if(isDragging)
				{
					for(int i = 0; i < selectedUnits.Count; i++)
					{
						UnitScript unit = selectedUnits[i].GetComponent<UnitScript>();
						unit.isSelected = false;
					}
					selectedUnits.Clear();

					Bounds viewportBounds = RectExtension.GetViewportBounds(cam, initTouch0Pos, touch0Pos);
					GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
					for(int i = 0; i < units.Length; i++)
					{
						if(viewportBounds.Contains(cam.WorldToViewportPoint(units[i].transform.position)))
						{
							UnitScript unit = units[i].GetComponent<UnitScript>();
							if(!unit.isSelected)
							{
								unit.isSelected = true;
								selectedUnits.Add(unit);
							}
						}
					}
				}
				isDragging = false;
				dragTimer = 0.0f;
			}
			else if(t0.phase == TouchPhase.Canceled)
			{
				isHolding = false;

				isDragging = false;
				dragTimer = 0.0f;
			}
		}
		#elif UNITY_EDITOR
		touch0Pos = Input.mousePosition;
		if(Input.GetMouseButtonDown(0) && !Input.GetMouseButton(1))
		{
			isHolding = true;
			initTouch0Pos = touch0Pos;
		}
		if(Input.GetMouseButtonUp(0))
		{
			isHolding = false;
			if(Vector2.Distance(initTouch0Pos, touch0Pos) <= pressDistanceThreshold)
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
						Move(hit.point);
					}
				}
			}
			else if(isDragging)
			{
				for(int i = 0; i < selectedUnits.Count; i++)
				{
					UnitScript unit = selectedUnits[i].GetComponent<UnitScript>();
					unit.isSelected = false;
				}
				selectedUnits.Clear();

				Bounds viewportBounds = RectExtension.GetViewportBounds(cam, initTouch0Pos, touch0Pos);
				GameObject[] units = GameObject.FindGameObjectsWithTag("Unit");
				for(int i = 0; i < units.Length; i++)
				{
					if(viewportBounds.Contains(cam.WorldToViewportPoint(units[i].transform.position)))
					{
						UnitScript unit = units[i].GetComponent<UnitScript>();
						if(!unit.isSelected)
						{
							unit.isSelected = true;
							selectedUnits.Add(unit);
						}
					}
				}
			}
			isDragging = false;
			dragTimer = 0.0f;
		}
		#endif
	}

	void OnGUI()
	{
		if(isDragging)
		{
			// Create a rect from both mouse positions
			Rect rect = RectExtension.GetScreenRect( initTouch0Pos, touch0Pos );
			RectExtension.DrawScreenRectFilled(rect, new Color( 0.8f, 0.8f, 0.95f, 0.25f ), 2, new Color( 0.8f, 0.8f, 0.95f ));
		}
	}

	public void Move(Vector3 pos)
	{
		if(selectedUnits.Count > 0)
		{
			if(selectedUnits.Count == 1)
			{
				selectedUnits[0].Move(pos);
			}
			else
			{
				if(selectionMode == SelectionMode.ClusterMode)
				{
					for(int i = 0; i < selectedUnits.Count; i++)
					{
						selectedUnits[i].Move(pos);
					}
				}
				else if(selectionMode == SelectionMode.FormationMode)
				{
					Vector3 center = Vector3.zero;
					Vector3[] membersPos = new Vector3[selectedUnits.Count];

					for(int i = 0; i < selectedUnits.Count; i++)
					{
						membersPos[i] = selectedUnits[i].transform.position;
						center += membersPos[i];
					}

					center /= selectedUnits.Count;

					for(int i = 0; i < selectedUnits.Count; i++)
					{
						Vector3 offset = membersPos[i] - center;
						selectedUnits[i].Move(pos + offset);
					}
				}
			}
		}
	}

	public void ToggleSelectionMode()
	{
		selectionMode = (SelectionMode)(((int)selectionMode + 1) % (int)SelectionMode.Total);
	}
}
