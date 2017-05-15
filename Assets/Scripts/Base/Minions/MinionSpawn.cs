using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class MinionSpawn : NetworkBehaviour {
	public Vector3 spawnOffset;

	void Start() {
		Invoke ("DetermineStartPos", 1f);
	}

	void DetermineStartPos() {
		if (hasAuthority) {
			transform.position = BaseMenu.localBase.transform.position + spawnOffset;
		} else {
			transform.position = BaseMenu.otherBase.transform.position + spawnOffset;
		}
	}
}
