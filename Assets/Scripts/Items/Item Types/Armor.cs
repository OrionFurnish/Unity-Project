using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Items/Armor", order = 3)]
public class Armor : Item {
	public int defense = 0;

	public override void Equip(Stats stats) {stats.Defense += defense;}
	public override void Unequip (Stats stats) {stats.Defense -= defense;}
}