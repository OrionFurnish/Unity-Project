using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : Controller {
	public static GameObject localPlayer;
	public GameObject cameraTarget;
	Vector3 targetMovePos;
	bool targetMove;
	bool moving;

	public override void OnStartLocalPlayer() {
		Camera.main.GetComponent<FollowObject>().SetTarget(cameraTarget, gameObject);
		localPlayer = gameObject;
	}

	void FixedUpdate() {
		if (isLocalPlayer) {
			moving = false;
			if(Input.GetKey(KeyCode.LeftShift) && !attacking) {
				Attack ();
				//if(isServer) {RpcAttack();}
				//else {Attack();}
			} else if(!attacking) {TryMove();}
			SetMoving(moving);
		}
	}

	void Attack() {
		this.attacking = true;
		anim.SetBool("Attacking", this.attacking);
		if(isServer) {RpcAttack();} 
		else {CmdAttack();}
	}

	[Command] void CmdAttack() {
		RpcAttack();
	}

	public override void TryMove() {
		if(Input.GetKey(KeyCode.Mouse0)) {
			RaycastHit hit;
			int layerMask = ~(1 << gameObject.layer); // Ignore gameObject.layer
			if(SF.MouseRaycast(out hit, layerMask) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !SF.GetWithinRange(hit.point, transform.position, .2f)) {
				if(Input.GetKey(KeyCode.LeftCommand)) {targetMove = true;}
				else if(!targetMove) {Move(hit.point);}
				if(targetMove) {targetMovePos = hit.point;}
			}
		} if(targetMove) {
			Move(targetMovePos);
			if(SF.GetWithinRange(transform.position, targetMovePos, .5f) || Input.GetKeyDown(KeyCode.Mouse0)) {
				targetMove = false;
			}
		}
	}

	public override void Move(Vector3 target) {
		moving = true;
		base.Move(target);
	}

	public void AdjustPos(float adjAmount) {
		StartCoroutine(Adjust(adjAmount));
	}

	IEnumerator Adjust(float adjAmount) {
		float startTime = Time.time;
		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
		float timeMult = state.length - state.length * (state.normalizedTime % 1);
		while (Time.time < startTime+timeMult) {
			transform.Translate((Vector3.forward*adjAmount*Time.deltaTime)/timeMult);
			yield return null;
		}
	}
}
