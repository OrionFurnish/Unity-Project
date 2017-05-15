using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class Controller : NetworkBehaviour {
	private Rigidbody rb;
	protected Animator anim;
	protected Targeting targeting;
	public GameObject relativeMoveBone;
	float turnRate = 10f;
	IEnumerator curRoutine;
	private Stats stats;
	protected ActionBehavior ab;
	public bool performing, attacking;
	[HideInInspector] public HitDetection hitDetect;
	public float baseSpeed;
	public AudioClip walkClip;
	private AudioSource audioSource;

	public virtual void Start() {
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator>();
		targeting = GetComponent<Targeting>();
		stats = GetComponent<Stats>();
		ab = GetComponent<ActionBehavior>();
		stats.Speed = baseSpeed;
		audioSource = GetComponent<AudioSource>();
	}

	void Update() {
		if(hasAuthority && !performing) {
			if(ab.TryAction()) {}
			else {TryMove();}
		}
	}

	public void ActivateDamage() {
		this.attacking = true;
	}

	public void DeactivateDamage() {
		this.attacking = false;
	}

	[Command] public void CmdStagger() {
		RpcStagger ();
	}

	[ClientRpc] void RpcStagger() {
		anim.SetTrigger ("Stagger");
	}

	public void TurnTowards(Vector3 target) {
		if(curRoutine != null) {StopCoroutine(curRoutine);}
		curRoutine = SF.TurnTowards(rb, target, turnRate);
		StartCoroutine(curRoutine);
	}

	public void SetMoving(Vector3 target) {
		Vector3 localDir = transform.InverseTransformDirection(SF.GetDirectionForce(relativeMoveBone.transform.position, target));
		if(float.IsNaN(localDir.z)) {anim.SetFloat("Walking", 0);}
		else{anim.SetFloat("Walking", localDir.z);}
	}

	public virtual void TryMove() {}

	public void PlayStepSound() {
		audioSource.PlayOneShot(walkClip);
	}

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
		if(hitDetect != null && hitDetect.gameObject.activeInHierarchy) {hitDetect.Reset();}
		performing = false;
	}

	IEnumerator adjustCoroutine;
	public void AdjustPos(float adjAmount) {
		if(adjustCoroutine != null) {StopCoroutine(adjustCoroutine);}
		adjustCoroutine = Adjust(adjAmount);
		StartCoroutine(adjustCoroutine);
	}

	IEnumerator Adjust(float adjAmount) {
		float startTime = Time.time;
		AnimatorStateInfo state = anim.GetCurrentAnimatorStateInfo (0);
		float timeMult = state.length - state.length * (state.normalizedTime % 1);
		while (Time.time < startTime+timeMult) {
			rb.MovePosition(rb.position+transform.TransformDirection((Vector3.forward*adjAmount*Time.fixedDeltaTime)/timeMult)*transform.localScale.x);
			yield return new WaitForFixedUpdate();
		}
	}
}
