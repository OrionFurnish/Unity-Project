using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootInventory : MonoBehaviour {
	private static LootInventory currentChest;
	public GameObject inventoryObj;
	private Inventory inventoryDisplay;
	public InvItem[] inventory;
	public ItemPool pool;
	public int minNumberOfItems;
	public int maxNumberOfItems;
	public float magicChance;
	public ModifierList modList;
	public AudioClip chestClip;
	private AudioSource audioSource;

	void Start() {
		inventoryDisplay = inventoryObj.GetComponentInChildren<Inventory>();
		inventoryObj.SetActive(false);
		inventory = new InvItem[inventoryDisplay.GetSlotCount()];
		GenerateItems();
		audioSource = GetComponent<AudioSource> ();
	}

	void Update() {
		if(PlayerController.localPlayer != null && SF.GetWithinRange(PlayerController.localPlayer.transform.position, transform.position, 1.5f)) {
			if(Input.GetKeyDown("e")) {
				if(inventoryObj.activeInHierarchy && currentChest == this) {CloseInventory();}
				else if(currentChest == null) {DisplayInventory();}
			}
		} else if(currentChest == this) {CloseInventory();}
	}

	public void DisplayInventory() {
		for(int i = 0; i < inventory.Length; i++) {
			inventoryDisplay.Set(i, inventory[i]);
		} inventoryObj.SetActive(true);
		currentChest = this;
		audioSource.PlayOneShot (chestClip);
	}

	public void CloseInventory() {
		for(int i = 0; i < inventory.Length; i++) {
			inventory[i] = inventoryDisplay.Get(i);
		} inventoryObj.SetActive(false);
		currentChest = null;
		audioSource.PlayOneShot (chestClip);
	}

	private void GenerateItems() {
		float totalDiscovery = 0f;
		foreach(Item item in pool.items) {
			totalDiscovery += item.discovery;
		} int numberOfItems = Random.Range(minNumberOfItems, maxNumberOfItems+1);
		for(int i = 0; i < numberOfItems; i++) {
			float itemFound = Random.Range(0, totalDiscovery);
			float curDiscovery = 0f;
			int index = 0;
			curDiscovery += pool.items[index].discovery;
			while(itemFound > curDiscovery) {
				index++;
				curDiscovery += pool.items[index].discovery;
			} inventory[i].item = pool.items[index];
			inventory[i].suffix = GenerateModifier();
			inventory[i].count = Random.Range(1, inventory[i].item.maxStackCount+1);
		}
	}

	private Modifier GenerateModifier() {
		float randMagic = Random.Range(0f, 100f);
		if (randMagic <= magicChance) {
			int randIndex = Random.Range (0, modList.modifierList.Length);
			return modList.modifierList [randIndex];
		} else {return null;}
	}
}
