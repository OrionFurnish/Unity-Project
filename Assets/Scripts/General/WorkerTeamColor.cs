using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WorkerTeamColor : NetworkBehaviour {
	public Material redMat, blueMat;

	public override void OnStartAuthority () {
		base.OnStartAuthority ();
		GetComponentInChildren<MeshRenderer>().material = redMat;
		gameObject.tag = "Team1";
	}

	void Start() {
		if(!hasAuthority) {
			GetComponentInChildren<MeshRenderer>().material = blueMat;
			gameObject.tag = "Team2";
		}
	}
}
