using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SetupLocalPlayer : NetworkBehaviour {

	Animator animator;
	[SyncVar (hook="OnChangeAnimation")] public string animState = "idle";

	[Command] public void CmdChangeAnimState(string aS)
	{
		UpdateAnimationState(aS);
	}

	void OnChangeAnimation(string aS)
	{
		if(isLocalPlayer) return;
		UpdateAnimationState(aS);
	}

	void UpdateAnimationState(string aS)
	{
		if(animState == aS) return;
		animState = aS;
		Debug.Log(animState);
		if(animState == "idle") 
			animator.SetBool("Idling", true);
		else if(animState == "run")
			animator.SetBool("Idling", false);
		else if(animState == "attack")
			animator.SetTrigger("Attacking");
	}

	void Start () {
		animator = GetComponent<Animator>(); 
		animator.SetBool("Idling", true);

		if(isLocalPlayer)
		{
			GetComponent<PlayerController>().enabled = true;
			CameraFollow360.player = this.gameObject.transform;
		}
		else
		{
			GetComponent<PlayerController>().enabled = false;			
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
