using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerActionBehavior : ActionBehavior {
	EquipSlot quickSlot;
	Targeting targeting;

	public override void Start() {
		base.Start();
		quickSlot = EquipSlot.equipSlots[6];
		targeting = GetComponent<Targeting>();
	}

	public override bool TryAction() {
		if(Input.GetKeyDown("1")) {quickSlot.Consume();}
		else if(Input.GetKeyDown("t")) {targeting.SetTarget();}
		else if(control.hitDetect
			!= null && Input.GetKey(KeyCode.LeftShift)
			&& playerBars.GetStamina() > 0f) 
		{Attack();}
		else {return false;}
		return true;
	}
}
