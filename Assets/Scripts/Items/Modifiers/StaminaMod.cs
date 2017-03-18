using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Modifier/Stamina Mod", order = 2)]
public class StaminaMod : Modifier {
	public float stamina;

	public override void Equip(Stats stats) {stats.MaxStam += stamina;}
	public override void Unequip(Stats stats) {stats.MaxStam -= stamina;}
	public override string GetTooltip() {return "+" + stamina + " Stamina";}
}
