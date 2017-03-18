using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Modifier/Speed Mod", order = 2)]
public class SpeedMod : Modifier {
	public float speedMult;

	public override void Equip(Stats stats) {stats.speedMult += speedMult;}
	public override void Unequip(Stats stats) {stats.speedMult -= speedMult;}
	public override string GetTooltip() {return "+" + speedMult*100 + "% Speed";}
}
