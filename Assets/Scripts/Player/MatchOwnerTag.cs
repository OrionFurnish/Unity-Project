using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchOwnerTag : MonoBehaviour {
	public GameObject owner;

	void Start () {
		Invoke ("MatchTag", 1f);
	}

	void MatchTag() {
		gameObject.tag = owner.tag;
	}
}
