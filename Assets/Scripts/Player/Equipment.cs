using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Equipment : NetworkBehaviour {
	private Dictionary<string, GameObject> itemRef;
	[SyncVar] string itemName = "";
	string prevName = "";

	void Start() {
		itemRef = new Dictionary<string, GameObject>();
		AddChildEquipment(transform);
	}

	void AddChildEquipment(Transform parent) {
		foreach(Transform child in parent) {
			if(child.tag == "Equipment") {
				itemRef.Add(child.name, child.gameObject);
				child.gameObject.SetActive(false);
			} if(child.childCount > 0) {AddChildEquipment(child);}
		}
	}

	void Update() {
		if(prevName != itemName) {Equip(itemName);}
	}

	private void Equip(string itemName) {
		if(itemRef.ContainsKey(itemName) || itemName == "") {
			if(prevName != "") {itemRef[prevName].SetActive(false);}
			if(itemName != "") {itemRef[itemName].SetActive(true);}
		} else {Debug.Log("Invalid model name: " + itemName);}
		prevName = itemName;
	}

	[Command] public void CmdEquip(string newName) {
		itemName = newName;
	}
}
