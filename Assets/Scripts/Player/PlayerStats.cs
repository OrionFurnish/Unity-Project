using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : Stats {
	public static Stats localInstance;

	public override void OnStartLocalPlayer() {
		localInstance = this;
	}
}
