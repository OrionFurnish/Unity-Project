using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	public float speed;
	public bool attacking;
	private Rigidbody rb;
	protected Animator anim;
	public HitDetection hitDetect;

	void Start() {
		rb = GetComponentInChildren<Rigidbody>();
		anim = GetComponentInChildren<Animator>();
	}

	public void SetMoving(bool moving) {
		anim.SetBool("Moving", moving);
	}
		
	[ClientRpc] public void RpcAttack() {
		if(!isLocalPlayer) {
			this.attacking = true;
			anim.SetBool("Attacking", this.attacking);
		}
	}

	public void FinishAttack() {
		this.attacking = false;
		anim.SetBool("Attacking", this.attacking);
		if(hitDetect.gameObject.activeInHierarchy) {hitDetect.Reset();}
	}

	public virtual void TryMove() {}

	public virtual void Move(Vector3 target) {
		if(target == transform.position) {return;}
		float adjSpd = speed * Time.fixedDeltaTime;
		target += new Vector3 (0, transform.position.y-target.y, 0);
		SF.MoveTowards(rb, target, adjSpd);
		transform.LookAt(target);
	}
}
