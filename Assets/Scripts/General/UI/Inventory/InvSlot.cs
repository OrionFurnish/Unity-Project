using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InvSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
	public static InvSlot overSlot;
	public Item startItem;
	protected Item item;
	private Image image;
	private bool selected = false;

	void Awake() {
		image = GetComponent<Image>();
		if(startItem != null) {ReplaceItem(startItem);}
	}

	void OnEnable() {
		image.color = Color.white;
		UpdateIcon();
	}

	public Item Get() {return item;}

	public void Swap(InvSlot slot) {
		if((slot.item == null || CheckRestrictions(slot.item)) && (item == null || slot.CheckRestrictions(item))) {
			slot.ReplaceItem(this.ReplaceItem(slot.item));
		}
	}

	protected virtual Item ReplaceItem(Item newItem) {
		Item oldItem = item;
		item = newItem;
		UpdateIcon();
		return oldItem;
	}

	protected virtual bool CheckRestrictions(Item item) {return true;}

	public void UpdateIcon() {
		if(item != null) {image.sprite = item.icon;}
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
		if(item != null) {
			selected = true;
			image.color = Color.red;
			PointerIconCtrl.Activate(item.icon);
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
