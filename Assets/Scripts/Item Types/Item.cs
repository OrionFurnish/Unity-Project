using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ItemType {Weapon, Armor}
public enum EquipType {None, Quickslot, OneHand, TwoHand, Head, Chest, Gloves, Legs}

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Item", order = 1)]
public class Item : ScriptableObject {
	public string itemName;
	public string modelName;
	public int value;
	public int maxStackCount;
	public Sprite icon;
	public ItemType itemType;
	public EquipType equipType;
	public float discovery;
	public string description;
}
