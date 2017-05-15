using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class PlayerController : Controller {
	public static GameObject localPlayer;
	public GameObject cameraTarget;
	Vector3 targetMovePos;
	bool targetMove;

	public override void Start() {
		base.Start();
		if(!isLocalPlayer) {
			GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	public override void OnStartLocalPlayer() {
		Camera.main.GetComponent<FollowObject>().SetTarget(cameraTarget, gameObject);
		localPlayer = gameObject;
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
}
