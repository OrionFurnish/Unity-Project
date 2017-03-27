using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BuySlot : Slot {
	private void Buy(Slot slot) {
		InvItem slotItem = slot.Get();
		if(slotItem.item == null && Resources.Gold >= invItem.GetValue()) {
			Resources.Gold -= invItem.GetValue();
			slot.AddItem(invItem);
			AddItem(slotItem);
		}
	}
		
	public override void OnPointerUp(PointerEventData eventData) {
		if(InvSlot.overSlot != null) {Buy(InvSlot.overSlot);}
		base.OnPointerUp(eventData);
	}
}
