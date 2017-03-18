using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipSlot : InvSlot {
	public static List<EquipSlot> equipSlots;
	public EquipType equippableType;
	public EquipmentDisplay equipDisplay;
	private Stats stats;

	void Start() {
		equipDisplay = PlayerController.localPlayer.GetComponent<EquipmentDisplay>();
		if(equipSlots == null) {equipSlots = new List<EquipSlot>();}
		equipSlots.Add(this);
		stats = PlayerController.localPlayer.GetComponent<Stats>();
	}

	public override InvItem ReplaceItem(InvItem newItem) {
		if(invItem.item != null) {invItem.Unequip(stats);}
		if(newItem.item != null) {
			newItem.Equip(stats);
			equipDisplay.EquipItem(newItem.item.equipType, newItem.item.modelName);
		} else {equipDisplay.EquipItem(equippableType, "");}
		return base.ReplaceItem(newItem);
	}

	protected override bool CheckRestrictions(Item item) {
		return equippableType == item.equipType;
	}
}
