using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFollowObject : MonoBehaviour {
	public GameObject target;
	public Vector3 offset;

	void Update() {
		if(target != null) {
			transform.position = Camera.main.WorldToScreenPoint(target.transform.position+offset);
		} else {Destroy(gameObject);}
	}
}
