using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : SoldierScript
{
	// Update is called once per frame
	public override void Update ()
	{
//		isRunning = Input.GetAxis("Horizontal") != 0.0f || Input.GetAxis("Vertical") != 0.0f;
//		isShooting = Input.GetKey(KeyCode.Space);
//
//		Vector3 dir = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")) * Time.deltaTime * speed;
//		transform.Translate(dir);

		UpdateAnimator();
		UpdateWeapon();
	}

	public override void ShootBullet()
	{
		Ray ray = new Ray(transform.position, transform.forward);
		RaycastHit hit;
		if(Physics.Raycast(ray, out hit, 500))
		{

		}
	}
}
