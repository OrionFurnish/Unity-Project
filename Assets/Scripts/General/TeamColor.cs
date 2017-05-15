using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TeamColor : NetworkBehaviour {
	public Material redMat, blueMat;

	public override void OnStartAuthority () {
		base.OnStartAuthority ();
		GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = redMat;
		gameObject.tag = "Team1";
	}

	void Start() {
		if(!hasAuthority) {
			GetComponentInChildren<SkinnedMeshRenderer>().sharedMaterial = blueMat;
			gameObject.tag = "Team2";
		}
	}
}
