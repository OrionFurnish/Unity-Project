using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TooltipControl : MonoBehaviour {
	public Vector3 tooltipOffset;
	GameObject tooltipObj;
	Text tooltipText;
	RectTransform tooltipRect;
	InvSlot slot;
	
	void Start() {
		tooltipObj = GameObject.Find("Tooltip Panel");
		tooltipText = GameObject.Find("Tooltip Text").GetComponent<Text>();
		tooltipRect = tooltipObj.GetComponent<RectTransform>();
	}

	void Update() {
		slot = InvSlot.overSlot;
		if(slot != null && slot.gameObject.activeInHierarchy) {
			InvItem invItem = slot.Get();
			if(!Input.GetMouseButton(0) && invItem.item != null) {
				tooltipObj.SetActive(true);
				tooltipText.text = ConvertItemToTooltip(invItem);
				Vector3 heightAdjust = new Vector3(0, (slot.GetComponent<RectTransform>().rect.height+tooltipRect.rect.height)/2, 0);
				tooltipObj.transform.position = slot.transform.position+tooltipOffset+heightAdjust;
			} else {tooltipObj.SetActive(false);}
		} else {tooltipObj.SetActive(false);}
	}

	string ConvertItemToTooltip(InvItem invItem) {
		Item item = invItem.item;
		Modifier suffix = null;
		if(invItem.suffix != null) {suffix = invItem.suffix;}
		string tooltipText = item.itemName;
		if(suffix != null) {tooltipText += " " + suffix.nameMod;}
		if(item.itemType == ItemType.Weapon) {
			Weapon weapon = (Weapon)item;
			tooltipText += "\n\nDamage: " + weapon.damage;
		} else if(item.itemType == ItemType.Armor) {
			Armor armor = (Armor)item;
			tooltipText += "\n\nDefense: " + armor.defense;
		} if(suffix != null) {tooltipText += "\n\n" + suffix.GetTooltip();}
		tooltipText += "\n\n" + item.description;
		tooltipText += "\n\n" + invItem.GetValue() + " Gold";
		return tooltipText;
	}
}
