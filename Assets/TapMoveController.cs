using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMoveController : MonoBehaviour
{
	Vector3 targetPosition = Vector3.zero;
	public bool mouseMode;
	public float speed = 10.0f;
	Vector3 offset;

	// Use this for initialization
	void Start ()
	{
		Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2f, Screen.height / 2f));
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 10000f))
		{
			offset = this.transform.position - hit.point;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		//#if UNITY_EDITOR
		if(mouseMode)
		{
			//Mouse Simulation
			if(Input.GetMouseButton(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 10000f))
				{
					targetPosition = hit.point;
					Debug.Log(hit.point);
				}
			}
		}
		//#elif UNITY_ANDROID
		else
		{
			//Touch Controls
			if(Input.touchCount > 0)
			{
				//if(Input.touches[0].phase == TouchPhase.Began)

				Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
				RaycastHit hit;
				if(Physics.Raycast(ray, out hit, 10000f))
				{
					targetPosition = hit.point;
					Debug.Log(hit.point);
				}
			}
		}
		//#endif
	}

	void LateUpdate()
	{
		this.transform.position = Vector3.MoveTowards(this.transform.position, targetPosition + offset, Time.deltaTime * speed);
	}
}
