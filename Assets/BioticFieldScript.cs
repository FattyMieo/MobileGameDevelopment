using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioticFieldScript : MonoBehaviour
{
	//Developer
	private Collider col;
	public List<SoldierScript> soldiers;

	[Header("Healing properties")]
	public int healAmount;
	public float expireTime;
	public float healingIntervalDuration;
	private float healingIntervalTimer;

	// Use this for initialization
	void Start ()
	{
		col = GetComponentInChildren<Collider>();
		Destroy(gameObject, expireTime);
	}
	
	// Update is called once per frame
	void Update ()
	{
		healingIntervalTimer += Time.deltaTime;
		if(healingIntervalTimer >= healingIntervalDuration)
		{
			for(int i = 0; i < soldiers.Count; i++)
			{
				soldiers[i].health += healAmount;
				if(soldiers[i].health > soldiers[i].maxHealth)
				{
					soldiers[i].health = soldiers[i].maxHealth;
				}
			}
			healingIntervalTimer = 0.0f;
		}
	}
}
