using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : Controller {
	public static GameObject localPlayer;
	public GameObject cameraTarget;
	Vector3 targetMovePos;
	bool targetMove;
	PlayerBars playerBars;
	EquipSlot quickSlot;

	public override void Start() {
		base.Start();
		playerBars = GetComponent<PlayerBars>();
		quickSlot = EquipSlot.equipSlots[6];
		if(!isLocalPlayer) {
			GetComponent<Rigidbody> ().isKinematic = true;
		}
	}

	public override void OnStartLocalPlayer() {
		Camera.main.GetComponent<FollowObject>().SetTarget(cameraTarget, gameObject);
		localPlayer = gameObject;
	}

	void Update() {
		if(isLocalPlayer) {
			if(Input.GetKeyDown("1")) {quickSlot.Consume();}
			if(Input.GetKey(KeyCode.LeftShift) && !attacking && playerBars.GetStamina() > 0f) {Attack();} 
			else if(!attacking) {
				if(Input.GetKeyDown("t")) {targeting.SetTarget();}
				TryMove();
			} 
		}
	}

	void Attack() {
		playerBars.CmdDrainStamina(5f);
		attacking = true;
		anim.SetTrigger("Attack");
		if(isServer) {RpcAttack();} 
		else {CmdAttack();}
	}

	[Command] void CmdAttack() {RpcAttack();}
	[ClientRpc] private void RpcAttack() {
		if(!isLocalPlayer) {
			attacking = true;
			anim.SetTrigger("Attack");
		}
	}

	public override void TryMove() {
		if(Input.GetKey(KeyCode.Mouse0)) {
			RaycastHit hit;
			int layerMask = ~(1 << gameObject.layer); // Ignore gameObject.layer
			if(SF.MouseRaycast(out hit, layerMask) && !UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject() && !SF.GetWithinRange(hit.point, transform.position, .2f) && !PointerIconCtrl.IsActive()) {
				if(Input.GetKey(KeyCode.LeftCommand)) {targetMove = true;}
				else if(!targetMove) {Move(hit.point);}
				if(targetMove) {targetMovePos = hit.point;}
			} else {SetMoving(relativeMoveBone.transform.position);}
		} else if(targetMove) {
			Move(targetMovePos);
			if(SF.GetWithinRange(transform.position, targetMovePos, .5f) || Input.GetKeyDown(KeyCode.Mouse0)) {
				targetMove = false;
			}
		} else {SetMoving(relativeMoveBone.transform.position);}
	}

	public override void Move(Vector3 target) {
		base.Move(target);
	}

	public void AdjustPos(float adjAmount) {StartCoroutine(Adjust(adjAmount));}
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
