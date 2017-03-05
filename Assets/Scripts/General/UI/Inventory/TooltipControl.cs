using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TooltipControl : MonoBehaviour {
	public Vector3 tooltipOffset;
	GameObject tooltipObj;
	Text tooltipText;
	RectTransform tooltipRect;
	InvSlot slot;
	
	void Start () {
		tooltipObj = GameObject.Find("Tooltip Panel");
		tooltipText = GameObject.Find("Tooltip Text").GetComponent<Text>();
		tooltipRect = tooltipObj.GetComponent<RectTransform>();
	}

	void Update() {
		slot = InvSlot.overSlot;
		if(slot != null && slot.gameObject.activeInHierarchy) {
			Item item = slot.Get();
			if(!Input.GetMouseButton(0) && item != null) {
				tooltipObj.SetActive(true);
				tooltipText.text = ConvertItemToTooltip(item);
				Vector3 heightAdjust = new Vector3(0, (slot.GetComponent<RectTransform>().rect.height+tooltipRect.rect.height)/2, 0);
				tooltipObj.transform.position = slot.transform.position+tooltipOffset+heightAdjust;
			} else {tooltipObj.SetActive(false);}
		} else {tooltipObj.SetActive(false);}
	}

	string ConvertItemToTooltip(Item item) {
		string tooltipText = item.itemName;
		if(item.itemType == ItemType.Weapon) {
			Weapon weapon = (Weapon)item;
			tooltipText += "\n\nDamage: " + weapon.damage;
		} else if(item.itemType == ItemType.Armor) {
			Armor armor = (Armor)item;
			tooltipText += "\n\nDefense: " + armor.defense;
		} tooltipText += "\n\n" + item.description;
		tooltipText += "\n\n" + item.value + " Gold";
		return tooltipText;
	}
}
