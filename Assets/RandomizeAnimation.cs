using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomizeAnimation : StateMachineBehaviour
{
	public int totalStands;
	public int totalAttacks;

	public float readyTimer;
	public float readyDuration;

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
//	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//	{
//	}

	// OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		if(animator.GetBool("IsAttacking"))
		{
			readyTimer = readyDuration;
		}

		bool standReady = readyTimer > 0.0f;
		if(standReady)
		{
			readyTimer -= Time.deltaTime;
		}

		animator.SetBool("StandReady", standReady);
	}

	// OnStateExit is called before OnStateExit is called on any state inside this state machine
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		animator.SetInteger("StandValue", Random.Range(0, totalStands * 2) / 2);
		animator.SetInteger("AttackValue", Random.Range(0, totalAttacks * 2) / 2);
	}

	// OnStateMove is called before OnStateMove is called on any state inside this state machine
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called before OnStateIK is called on any state inside this state machine
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMachineEnter is called when entering a statemachine via its Entry Node
	//override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash){
	//
	//}

	// OnStateMachineExit is called when exiting a statemachine via its Exit Node
	//override public void OnStateMachineExit(Animator animator, int stateMachinePathHash) {
	//
	//}
}
