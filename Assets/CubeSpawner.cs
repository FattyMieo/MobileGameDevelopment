using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
	public GameObject spawnedGO;
	public int amount;
	public Vector3 start;
	public Vector3 end;

	// Use this for initialization
	void Start ()
	{
		for(int i = 0; i < amount; i++)
		{
			Instantiate
			(
				spawnedGO,
				new Vector3
				(
					Random.Range(transform.position.x + start.x, transform.position.x + end.x),
					Random.Range(transform.position.y + start.y, transform.position.y + end.y),
					Random.Range(transform.position.z + start.z, transform.position.z + end.z)
				),
				Quaternion.identity
			);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
