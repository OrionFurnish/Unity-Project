using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Modifier/Defense Mod", order = 2)]
public class DefenseMod : Modifier {
	public float defense;

	public override void Equip(Stats stats) {stats.Defense += defense;}
	public override void Unequip(Stats stats) {stats.Defense -= defense;}
	public override string GetTooltip() {return "+" + defense + " Defense";}
}
