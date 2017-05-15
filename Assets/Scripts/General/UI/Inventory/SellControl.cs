using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SellControl : MonoBehaviour {
	[SerializeField] Text goldText;
	InvSlot[] slots;
	float totalValue;

	void Start() {
		slots = GetComponentsInChildren<InvSlot>();
	}

	void Update() {
		UpdateTotalValue();
	}

	public void SellItems() {
		Resources.Gold += totalValue;
		for(int i = 0; i < slots.Length; i++) {
			slots[i].AddItem(new InvItem());
		}
	}

	public void Cancel() {

	}

	private void UpdateTotalValue() {
		totalValue = 0;
		for(int i = 0; i < slots.Length; i++) {
			InvItem item = slots[i].Get();
			if(item.item != null) {totalValue += item.GetValue();}
		} goldText.text = totalValue + " Gold";
	}
}
