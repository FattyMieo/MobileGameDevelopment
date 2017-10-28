using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxScript : MonoBehaviour {

	public SoldierScript self;
	public float multiplier;

	void Start()
	{
		self = GetComponentInParent<SoldierScript>();
	}

	public void TakeDamage(int damage)
	{
		self.TakeDamage(Mathf.RoundToInt(damage * multiplier));
	}
}
