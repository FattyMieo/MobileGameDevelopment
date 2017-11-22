using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour, IMoveable
{
	private Animator anim;
	private NavMeshAgent navAgent;

	[Header("Settings")]
	public GameObject selectionQuad;

	[Header("Status")]
	public bool isSelected;
	private bool lastSelected;

	public bool isAttacking;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponentInChildren<Animator>();
		navAgent = GetComponent<NavMeshAgent>();

		lastSelected = isSelected;
		selectionQuad.SetActive(isSelected);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(lastSelected != isSelected)
		{
			selectionQuad.SetActive(isSelected);
			lastSelected = isSelected;
		}

		anim.SetFloat("Velocity", navAgent.velocity.sqrMagnitude);

		//Debug
		if(Input.GetKeyDown(KeyCode.Space))
		{
			isAttacking = !isAttacking;
			anim.SetBool("IsAttacking", isAttacking);
		}
	}

	public void Move(Vector3 pos)
	{
		navAgent.SetDestination(pos);
	}
}
