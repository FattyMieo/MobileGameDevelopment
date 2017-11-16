using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitScript : MonoBehaviour
{
	public Shader normalShader;
	public Shader highlightShader;
	private Renderer rend;

	public bool isSelected;
	private NavMeshAgent navAgent;
	// Use this for initialization
	void Start ()
	{
		rend = GetComponent<Renderer>();
		navAgent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(isSelected)
		{
			rend.material.shader = highlightShader;
		}
		else
		{
			rend.material.shader = normalShader;
		}
	}

	public void Move(Vector3 pos)
	{
		navAgent.SetDestination(pos);
	}
}
