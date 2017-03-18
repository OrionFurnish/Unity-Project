using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Modifier : ScriptableObject {
	public string nameMod;
	public float valueMult;

	public virtual void Equip(Stats stats) {}
	public virtual void Unequip(Stats stats) {}
	public virtual string GetTooltip() {return "";}
}