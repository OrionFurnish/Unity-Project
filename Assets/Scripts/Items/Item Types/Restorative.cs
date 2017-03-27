using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Custom/Items/Restorative", order = 3)]
public class Restorative : Item {
	public float life;
	public float time;

	public override void Consume() {
		PlayerBars.localInstance.CmdRestoreLife(life, time);
	}
}
