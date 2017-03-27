using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	private Slot[] slots;

	void Awake() {
		slots = GetComponentsInChildren<Slot>();
	}

	public InvItem Get(int slot) {
		return slots[slot].Get();
	}

	public int GetSlotCount() {return slots.Length;}

	public void Set(int slot, InvItem item) {
		slots[slot].AddItem(item);
	}
}
