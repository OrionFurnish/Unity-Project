using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Minions/Minion", order = 1)]
public class Minion : ScriptableObject {
	public string minionName;
	public GameObject prefab;
	public float cost;
	public string description;
}
