using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EquipmentDisplay : NetworkBehaviour {
	private Dictionary<string, GameObject> itemRef;
	string prevWeapon = "";
	string prevHead = "";

	public void EquipItem(EquipType type, string newName) {
		switch(type) {
		case EquipType.HandOne:
			CmdEquipWeapon(newName);
			break;
		case EquipType.Head:
			CmdEquipHead(newName);
			break;
		}
	}

	[ClientRpc] private void RpcEquipWeapon(string newName) {
		if(itemRef.ContainsKey(newName) || newName == "") {
			if(prevWeapon != "") {itemRef[prevWeapon].SetActive(false);}
			if(newName != "") {itemRef[newName].SetActive(true);}
		} else {Debug.Log("Invalid model name: " + newName);}
		prevWeapon = newName;
	}

	[ClientRpc] private void RpcEquipHead(string newName) {
		if(itemRef.ContainsKey(newName) || newName == "") {
			if(prevHead != "") {itemRef[prevHead].SetActive(false);}
			if(newName != "") {itemRef[newName].SetActive(true);}
		} else {Debug.Log("Invalid model name: " + newName);}
		prevHead = newName;
	}

	[Command] private void CmdEquipWeapon(string newName) {RpcEquipWeapon(newName);}
	[Command] private void CmdEquipHead(string newName) {RpcEquipHead(newName);}

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
} 
