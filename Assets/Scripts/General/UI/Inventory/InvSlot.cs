using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public struct InvItem {
	public Item item;
	public int count;
	public Modifier suffix;

	public void Equip(Stats stats) {
		item.Equip(stats);
		if(suffix != null) {suffix.Equip(stats);}
	}

	public void Unequip(Stats stats) {
		item.Unequip(stats);
		if(suffix != null) {suffix.Unequip(stats);}
	}

	public int GetValue() {
		float mult = 1f;
		if(suffix != null) {mult = suffix.valueMult;}
		return Mathf.RoundToInt(item.value*mult);
	}
}

public class InvSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
	public static InvSlot overSlot;
	public InvItem startItem;
	protected InvItem invItem;
	private Image image;
	private bool selected = false;

	void Awake() {
		image = GetComponent<Image>();
		if(startItem.item != null) {ReplaceItem(startItem);}
	}

	void OnEnable() {
		image.color = Color.white;
		UpdateIcon();
	}

	public InvItem Get() {return invItem;}

	public void Swap(InvSlot slot) {
		if((slot.invItem.item == null || CheckRestrictions(slot.invItem.item)) && (invItem.item == null || slot.CheckRestrictions(invItem.item))) {
			slot.ReplaceItem(this.ReplaceItem(slot.invItem));
		}
	}

	public virtual InvItem ReplaceItem(InvItem newItem) {
		InvItem oldItem = invItem;
		invItem = newItem;
		UpdateIcon();
		return oldItem;
	}

	protected virtual bool CheckRestrictions(Item item) {return true;}

	public void UpdateIcon() {
		if(invItem.item != null) {image.sprite = invItem.item.icon;}
		else {image.sprite = null;}
	}

	public void OnPointerEnter(PointerEventData eventData) {
		overSlot = this;
		image.color = Color.yellow;
	}

	public void OnPointerExit(PointerEventData eventData) {
		overSlot = null;
		if(!selected) {image.color = Color.white;}
	}

	public void OnPointerDown(PointerEventData eventData) {
		if(invItem.item != null) {
			selected = true;
			image.color = Color.red;
			PointerIconCtrl.Activate(invItem.item.icon);
		}
	}

	public void OnPointerUp(PointerEventData eventData) {
		if(overSlot != null) {
			Swap(overSlot);
		} image.color = Color.white;
		selected = false;
		PointerIconCtrl.Deactivate();
	}
}
