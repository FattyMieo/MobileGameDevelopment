using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
	public Shader normalShader;
	public Shader highlightShader;
	private Renderer rend;
	private Animator anim;

	public bool isSelected;
	private NavMeshAgent navAgent;

	public bool isAttacking;

	// Use this for initialization
	void Start ()
	{
//		rend = GetComponent<Renderer>();
		anim = GetComponentInChildren<Animator>();
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isSelected)
		{
//			rend.material.shader = highlightShader;
		}
		else
		{
//			rend.material.shader = normalShader;
		}

		anim.SetFloat("Velocity", navAgent.velocity.sqrMagnitude);

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
