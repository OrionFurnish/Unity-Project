using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class BaseMenu : NetworkBehaviour {
	public Minion[] minions;
	public GameObject goldMine;
	public int spawnLimit;
	GameObject baseMenuObj;
	int spawnCount;
	public GameObject player;
	public Vector3 offset;
	public static GameObject localBase;
	public static GameObject otherBase;

	public override void OnStartAuthority () {
		base.OnStartAuthority ();
		baseMenuObj = GameObject.Find("Base Menu");
		baseMenuObj.SetActive(false);
		baseMenuObj.GetComponent<BaseMenuButtons>().menu = this;
		GetComponentInChildren<MeshRenderer>().material = PlayerController.localPlayer.GetComponent<TeamColor>().redMat;
		gameObject.tag = "Team1";
		EstablishWin.team1Base = gameObject;
		localBase = gameObject;
		transform.position = PlayerController.localPlayer.transform.position+offset;
	}

	void Start() {
		if (!hasAuthority) {
			GetComponentInChildren<MeshRenderer> ().material = PlayerController.localPlayer.GetComponent<TeamColor> ().blueMat;
			gameObject.tag = "Team2";
			EstablishWin.team2Base = gameObject;
			otherBase = gameObject;
		} else {
			CmdSetPos (transform.position);
		}
	}

	[Command] void CmdSetPos(Vector3 newPos) {
		RpcSetPos (newPos);
	}

	[ClientRpc] void RpcSetPos(Vector3 newPos) {
		transform.position = newPos;
	}

	void Update () {
		if(hasAuthority) {
			if(PlayerController.localPlayer != null && SF.GetWithinRange(PlayerController.localPlayer.transform.position, transform.position, 3f)) {
				if(Input.GetKeyDown("e")) {
					if(baseMenuObj.activeInHierarchy) {baseMenuObj.SetActive(false);}
					else {baseMenuObj.SetActive(true);}
				}
			} else {baseMenuObj.SetActive(false);}
		}
	}

	public void CreateWorker() {
		if(spawnCount < spawnLimit && Resources.Gold >= minions[0].cost) {
			CmdSpawnWorker();
			spawnCount++;
			Resources.Gold -= minions[0].cost;
		}
	}

	public void CreateSoldier() {
		if(spawnCount < spawnLimit && Resources.Gold >= minions[1].cost) {
			CmdSpawnSoldier ();
			spawnCount++;
			Resources.Gold -= minions[1].cost;
		}
	}

	[Command] void CmdSpawnSoldier() {
		GameObject soldier = Instantiate(minions[1].prefab, transform.position, Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(soldier, player);
	}

	[Command] void CmdSpawnWorker() {
		GameObject worker = Instantiate(minions[0].prefab, transform.position, Quaternion.identity);
		NetworkServer.SpawnWithClientAuthority(worker, player);
	}
}
