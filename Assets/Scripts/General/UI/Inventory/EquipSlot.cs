using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : InvSlot {
	public List<EquipType> equippableTypes;
	public Equipment equipment;

	void Start() {
		equipment = PlayerController.localPlayer.GetComponent<Equipment>();
	}

	protected override Item ReplaceItem(Item newItem) {
		if(newItem != null) {equipment.CmdEquip(newItem.modelName);}
		else {equipment.CmdEquip("");}
		return base.ReplaceItem(newItem);
	}

	protected override bool CheckRestrictions(Item item) {
		return equippableTypes.Count == 0 || equippableTypes.Contains(item.equipType);
	}
}
