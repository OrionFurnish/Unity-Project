using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ActionBehavior : NetworkBehaviour {
	protected PlayerBars playerBars;
	protected Animator anim;
	protected Controller control;

	public virtual void Start() {
		anim = GetComponent<Animator>();
		playerBars = GetComponent<PlayerBars>();
		control = GetComponent<Controller>();
	}

	public virtual bool TryAction() {
		return false;
	}

	public void AllowQueueNextAttack() {}

	protected void Attack() {
		playerBars.CmdDrainStamina(5f);
		control.performing = true;
		anim.SetTrigger("Attack");
		if(isServer) {RpcAttack();} 
		else {CmdAttack();}
	}

	[Command] protected void CmdAttack() {RpcAttack();}
	[ClientRpc] protected void RpcAttack() {
		if(!hasAuthority) {
			control.performing = true;
			anim.SetTrigger("Attack");
		}
	}
}
