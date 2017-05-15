using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionControl : Controller {
	public GameObject target;

	public override void TryMove() {
		if(target == null) {
			foreach(Collider coll in Physics.OverlapSphere(transform.position, 10f)) {
				Rigidbody rb = coll.attachedRigidbody;
				if(rb != null && rb.tag == "Team2" && rb.gameObject.layer == 8) {
					target = coll.attachedRigidbody.gameObject.GetComponent<Targeting>().targetableObj;
					targeting.SetTarget(target);
				}
			}
		} else if(target != null && !SF.GetWithinRange(transform.position, target.transform.position, 1f)) {
			Move(target.transform.position);
		} else {SetMoving(transform.position);}
	}
}
