using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CapsuleNavAI : MonoBehaviour
{
	NavMeshAgent navMeshAgent;
	public Transform target;

	// Use this for initialization
	void Start ()
	{
		navMeshAgent = this.GetComponent<NavMeshAgent>();

		StartCoroutine("RepathRoutine");
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	IEnumerator RepathRoutine()
	{
		while(true)
		{
			yield return new WaitForSeconds(0.5f);
			navMeshAgent.SetDestination(target.position);
		}
	}
}
