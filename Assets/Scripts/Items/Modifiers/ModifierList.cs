using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Modifier/List", order = 1)]
public class ModifierList : ScriptableObject {
	public Modifier[] modifierList;
}
