using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpawnBase : NetworkBehaviour {
	public GameObject basePrefab;

	public override void OnStartLocalPlayer() {
		CmdSpawnBase();
	}

	[Command] void CmdSpawnBase() {
		GameObject baseObj = Instantiate (basePrefab, transform.position, Quaternion.identity);
		baseObj.GetComponent<BaseMenu> ().player = gameObject;
		NetworkServer.SpawnWithClientAuthority (baseObj, gameObject);
	}
}
