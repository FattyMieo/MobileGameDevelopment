using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanController : MonoBehaviour
{
	Animator anim;
	public Transform ikPoi; //Point of interest
	public Transform ikHintPoi;
	public float ikPosWeight;

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void OnAnimatorIK (int layerIndex)
	{
		anim.SetIKPosition(AvatarIKGoal.RightHand, ikPoi.position);
		anim.SetIKPositionWeight(AvatarIKGoal.RightHand, ikPosWeight);
		anim.SetIKHintPosition(AvatarIKHint.RightElbow, ikHintPoi.position);
		anim.SetIKHintPositionWeight(AvatarIKHint.RightElbow, 1f);
	}

	public void PlayFootstepSFX()
	{
		Debug.Log("PlayFootstepSFX");
	}
}
