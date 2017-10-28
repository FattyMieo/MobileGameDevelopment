using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BioticFieldRegisterSoldier : MonoBehaviour
{
	private BioticFieldScript self;

	void Start()
	{
		self = GetComponentInParent<BioticFieldScript>();
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.GetComponent<SoldierScript>())
		{
			self.soldiers.Add(other.GetComponent<SoldierScript>());
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.GetComponent<SoldierScript>())
		{
			self.soldiers.Remove(other.GetComponent<SoldierScript>());
		}
	}
}
