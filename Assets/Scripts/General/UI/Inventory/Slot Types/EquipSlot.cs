using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : InvSlot {
	public static List<EquipSlot> equipSlots;
	public EquipType equippableType;
	public EquipmentDisplay equipDisplay;

	public override void Awake() {
		base.Awake();
		equipDisplay = PlayerController.localPlayer.GetComponent<EquipmentDisplay>();
		if(equipSlots == null) {equipSlots = new List<EquipSlot>();}
		equipSlots.Add(this);
	}

	protected override InvItem ReplaceItem(InvItem newItem) {
		if(invItem.item != null) {invItem.Unequip();}
		if(newItem.item != null) {
			newItem.Equip();
			equipDisplay.EquipItem(newItem.item.equipType, newItem.item.modelName);
		} else {equipDisplay.EquipItem(equippableType, "");}
		return base.ReplaceItem(newItem);
	}

	protected override bool CheckRestrictions(Item item) {
		return equippableType == item.equipType;
	}

	protected override void UpdateCount() {
		base.UpdateCount();
		if(invItem.count < 1 && invItem.item != null) {invItem.Unequip();}
	}
}
