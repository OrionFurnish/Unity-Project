using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	public bool attacking;
	private Rigidbody rb;
	protected Animator anim;
	[HideInInspector] public HitDetection hitDetect;
	protected Targeting targeting;
	public GameObject relativeMoveBone;
	float turnRate = 10f;
	IEnumerator curRoutine;
	private Stats stats;

	public virtual void Start() {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		targeting = GetComponent<Targeting>();
		stats = GetComponent<Stats>();
		stats.Speed = 700f;
	}

	public void TurnTowards(Vector3 target) {
		if(curRoutine != null) {StopCoroutine(curRoutine);}
		curRoutine = SF.TurnTowards(transform, target, turnRate);
		StartCoroutine(curRoutine);
	}

	public void SetMoving(Vector3 target) {
		Vector3 localDir = transform.InverseTransformDirection(SF.GetDirectionForce(relativeMoveBone.transform.position, target));
		if(float.IsNaN(localDir.z)) {anim.SetFloat("Walking", 0);}
		else{anim.SetFloat("Walking", localDir.z);}
	}

	public virtual void TryMove() {}

	public virtual void Move(Vector3 target) {
		SetMoving(target);
		if(target == transform.position) {return;}
		float adjSpd = stats.Speed * Time.deltaTime;
		target.y = 0;
		SF.MoveTowards(rb, target, adjSpd, relativeMoveBone.transform.position);
		if(!targeting.hasTarget()) {
			TurnTowards(target);
		}
	}

	public void ResetHitDetect() {
		if(hitDetect.gameObject.activeInHierarchy) {hitDetect.Reset();}
		attacking = false;
	}

	public void AllowQueueNextAttack() {}
}
