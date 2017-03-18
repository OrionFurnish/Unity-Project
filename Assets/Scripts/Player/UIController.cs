using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UIController : NetworkBehaviour {
	public static UIController instance;
	public Canvas canvas;
	public GameObject inventoryPrefab;
	public Transform weaponParent;
	GameObject invObj;

	public override void OnStartLocalPlayer() {
		instance = this;
		invObj = Instantiate(inventoryPrefab, GameObject.Find("Canvas").transform, false);
		invObj.transform.SetSiblingIndex(1);
		invObj.SetActive(false);
	}

	void Update() {
		if(isLocalPlayer && Input.GetKeyDown("i")) {
			if (invObj.activeInHierarchy) {invObj.SetActive(false);}
			else {invObj.SetActive(true);}
		}
	}
}
