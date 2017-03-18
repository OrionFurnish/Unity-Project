using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Items/Item Pool", order = 1)]
public class ItemPool : ScriptableObject {
	public Item[] items;
}
