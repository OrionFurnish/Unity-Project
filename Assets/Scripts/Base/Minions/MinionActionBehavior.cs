using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MinionActionBehavior : ActionBehavior {
	protected MinionControl minionControl;

	public override void Start() {
		base.Start();
		minionControl = GetComponent<MinionControl>();
	}

	public override bool TryAction() {
		if(minionControl.target != null && SF.GetWithinRange(transform.position, minionControl.target.transform.position, 1.5f)) {
			Attack();
			return true;
		} else {return false;}
	}


}
