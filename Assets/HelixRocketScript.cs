using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelixRocketScript : MonoBehaviour
{

	[Header("Rocket Settings")]
	public float flightSpeed;
	public int damage;

	[Header("Explosion Settings")]
	public GameObject explosionPrefab;
	public float explosionRadius;
	public float explosionForce;
	public float upwardsModifier;
	
	// Update is called once per frame
	void FixedUpdate ()
	{
		transform.Translate(Vector3.forward * Time.deltaTime * flightSpeed);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = new Color(0, 0, 1, 0.1f);
		Gizmos.DrawSphere(transform.position, explosionRadius);
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.layer != LayerMask.NameToLayer("Ignore Raycast"))
		{
			List<Rigidbody> registeredRb = new List<Rigidbody>();
			Collider[] hitCols = Physics.OverlapSphere(transform.position, explosionRadius, ~LayerMask.GetMask("Ignore Raycast"));
			for(int i = 0; i < hitCols.Length; i++)
			{
				if(hitCols[i].GetComponentInParent<Rigidbody>())
				{
					Rigidbody hitRb = hitCols[i].GetComponentInParent<Rigidbody>();
					if(!registeredRb.Contains(hitRb))
					{
						registeredRb.Add(hitRb);
						hitRb.AddExplosionForce(explosionForce, transform.position, explosionRadius, upwardsModifier, ForceMode.Impulse);

						if(hitRb.GetComponent<SoldierScript>())
						{
							SoldierScript soldier = hitRb.GetComponent<SoldierScript>();
							soldier.TakeDamage(damage);
						}
					}
				}
			}
			Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
}
