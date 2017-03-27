using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public struct InvItem {
	public Item item;
	public int count;
	public Modifier suffix;

	public void Equip() {
		item.Equip(Stats.localInstance);
		if(suffix != null) {suffix.Equip(Stats.localInstance);}
	}

	public void Unequip() {
		item.Unequip(Stats.localInstance);
		if(suffix != null) {suffix.Unequip(Stats.localInstance);}
	}

	public int GetValue() {
		float mult = 1f;
		if(suffix != null) {mult = suffix.valueMult;}
		return Mathf.RoundToInt(item.value*mult);
	}
}

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler {
	public InvItem startItem;
	protected InvItem invItem;
	private Image image;
	private bool selected = false;
	private Text countText;

	public virtual void Awake() {
		image = GetComponent<Image>();
		if(startItem.item != null) {AddItem(startItem);}
		countText = GetComponentInChildren<Text>();
	}

	void OnEnable() {
		image.color = Color.white;
		UpdateIcon();
	}

	public InvItem Get() {return invItem;}

	public void AddItem(InvItem newItem) {
		invItem = newItem;
		UpdateCount();
		UpdateIcon();
	}

	protected void RemoveItem() {
		invItem = new InvItem();
		UpdateCount();
		UpdateIcon();
	}

	protected void StackOnSlot(InvSlot slot) {
		int missing = slot.invItem.item.maxStackCount-slot.invItem.count;
		while(missing > 0 && invItem.count > 0) {
			slot.invItem.count++;
			invItem.count--;
			missing--;
		} UpdateCount();
		slot.UpdateCount();
	}

	protected virtual void UpdateCount() {
		if(countText != null) {
			if(invItem.count >= 2) {countText.text = invItem.count.ToString();}
			else {countText.text = "";}
		} if(invItem.count < 1 && invItem.item != null) {
			invItem.item = null;
			UpdateIcon();
		}
	}

	protected void UpdateIcon() {
		if(invItem.item != null) {
			image.sprite = invItem.item.icon;
		} else {image.sprite = null;}
	}

	public virtual void OnPointerEnter(PointerEventData eventData) {
		TooltipControl.slot = this;
		image.color = Color.yellow;
	}

	public virtual void OnPointerExit(PointerEventData eventData) {
		TooltipControl.slot = null;
		if(!selected) {image.color = Color.white;}
	}

	public void OnPointerDown(PointerEventData eventData) {
		if(invItem.item != null) {
			selected = true;
			image.color = Color.red;
			PointerIconCtrl.Activate(invItem.item.icon);
		}
	}

	public virtual void OnPointerUp(PointerEventData eventData) {
		image.color = Color.white;
		selected = false;
		PointerIconCtrl.Deactivate();
	}
}
