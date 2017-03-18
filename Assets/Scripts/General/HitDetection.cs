using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HitDetection : NetworkBehaviour {
	public string targetTag;
	public GameObject owner;
	private Controller control;
	private List<GameObject> hitObjs;
	private Stats stats;

	void Awake() {
		hitObjs = new List<GameObject>();
		control = owner.GetComponent<Controller>();
		stats = owner.GetComponent<Stats>();
	}

	void OnEnable() {
		if(control != null) {control.hitDetect = this;}
	}

	public void OnCollisionEnter(Collision coll) {
		if(control.attacking && coll.gameObject.tag == targetTag && coll.gameObject != owner && !hitObjs.Contains(coll.gameObject) && coll.gameObject.GetComponent<NetworkIdentity>().hasAuthority) {
			coll.gameObject.GetComponent<PlayerBars>().CmdTakeDamage(stats.Damage);
			hitObjs.Add(coll.gameObject);
		}
	}

	public void Reset() {hitObjs.Clear();}
}
