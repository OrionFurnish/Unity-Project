using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Modifier/Damage Mod", order = 2)]
public class DamageMod : Modifier {
	public float damage;

	public override void Equip(Stats stats) {stats.Damage += damage;}
	public override void Unequip(Stats stats) {stats.Damage -= damage;}
	public override string GetTooltip() {return "+" + damage + " Damage";}
}
