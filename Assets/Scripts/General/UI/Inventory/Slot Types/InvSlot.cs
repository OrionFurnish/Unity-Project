using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvSlot : Slot {
	public static InvSlot overSlot;

	public void Swap(InvSlot slot) {
		if((slot.invItem.item == null || CheckRestrictions(slot.invItem.item)) && (invItem.item == null || slot.CheckRestrictions(invItem.item))) {
			if(slot.invItem.item == invItem.item && slot.invItem.count < slot.invItem.item.maxStackCount) {StackOnSlot(slot);} 
			else {slot.ReplaceItem(this.ReplaceItem(slot.invItem));}
		}
	}

	protected virtual InvItem ReplaceItem(InvItem newItem) {
		InvItem oldItem = invItem;
		invItem = newItem;
		UpdateCount();
		UpdateIcon();
		return oldItem;
	}

	protected virtual bool CheckRestrictions(Item item) {return true;}

	public void Consume() {
		if(invItem.item != null) {
			invItem.item.Consume();
			invItem.count--;
			UpdateCount();
		}
	}

	public override void OnPointerEnter(PointerEventData eventData) {
		overSlot = this;
		base.OnPointerEnter(eventData);
	}

	public override void OnPointerExit(PointerEventData eventData) {
		overSlot = null;
		base.OnPointerExit(eventData);
	}

	public override void OnPointerUp(PointerEventData eventData) {
		if(overSlot != null) {
			Swap(overSlot);
		} base.OnPointerUp(eventData);
	}
}
