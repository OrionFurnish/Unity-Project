using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Items/Weapon", order = 3)]
public class Weapon : Item {
	public int damage = 0;

	public override void Equip(Stats stats) {stats.Damage += damage;}
	public override void Unequip (Stats stats) {stats.Damage -= damage;}
}