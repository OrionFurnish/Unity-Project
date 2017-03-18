using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Modifier/Life Mod", order = 2)]
public class LifeMod : Modifier {
	public float life;

	public override void Equip(Stats stats) {stats.MaxLife += life;}
	public override void Unequip(Stats stats) {stats.MaxLife -= life;}
	public override string GetTooltip() {return "+" + life + " Life";}
}
