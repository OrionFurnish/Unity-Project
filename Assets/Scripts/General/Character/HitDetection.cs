using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HitDetection : NetworkBehaviour {
	public string targetTag;
	public AudioClip hitClip;
	public GameObject owner;
	private Controller control;
	private List<GameObject> hitObjs;
	private Stats stats;
	private AudioSource audioSource;

	void Awake() {
		hitObjs = new List<GameObject>();
		control = owner.GetComponent<Controller>();
		stats = owner.GetComponent<Stats>();
		audioSource = owner.GetComponent<AudioSource>();
		if (owner.tag == "Team1") {targetTag = "Team2";}
		else {targetTag = "Team1";}
	}

	void OnEnable() {
		if(control != null) {control.hitDetect = this;}
	}

	public void OnTriggerEnter(Collider coll) {
		if(coll.attachedRigidbody != null) {
			GameObject hitObj = coll.attachedRigidbody.gameObject;
			if(control.attacking && hitObj.tag == targetTag && hitObj != owner && !hitObjs.Contains(hitObj) && hitObj.GetComponent<NetworkIdentity>().hasAuthority) {
				hitObj.GetComponent<PlayerBars>().CmdTakeDamage(stats.Damage);
				hitObj.GetComponent<Controller>().CmdStagger();
				hitObjs.Add(hitObj);
				audioSource.PlayOneShot(hitClip);
			}
		} else {
			GameObject hitObj = coll.gameObject;
			Debug.Log ("1");
			if(control.attacking && hitObj.tag == targetTag && !hitObjs.Contains(hitObj) && hitObj.GetComponent<NetworkIdentity>().hasAuthority) {
				Debug.Log ("2");
				hitObj.GetComponent<LifeBar>().CmdTakeDamage(stats.Damage);
				hitObjs.Add(hitObj);
				audioSource.PlayOneShot(hitClip);
			}
		}
	}

	public void Reset() {hitObjs.Clear();}
}
