using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ItemType {Weapon, Armor}
public enum EquipType {None, HandOne, HandTwo, Head, Chest, Gloves, Legs, Belt}

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Items/Item", order = 2)]
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

	public virtual void Equip(Stats stats) {}
	public virtual void Unequip(Stats stats) {}
}
