using UnityEngine;
using System.Collections;

public class FollowObject: MonoBehaviour {
	GameObject target;
	GameObject yTarget;
	Vector3 offset;

	public void SetTarget(GameObject target) {
		this.target = target;
		offset = transform.position-target.transform.position;
	}

	public void SetTarget(GameObject target, GameObject yTarget) {
		this.target = target;
		this.yTarget = target;
		offset = transform.position-new Vector3(0, yTarget.transform.position.y, 0);
	}

	void Update () {
		if(target != null) {
			if(yTarget != null) {
				transform.position = new Vector3(target.transform.position.x, yTarget.transform.position.y, target.transform.position.z)+offset;
			} else {transform.position = target.transform.position+offset;}
		}
	}
}
