using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stats: NetworkBehaviour {
	public static Stats localInstance;

	public override void OnStartLocalPlayer() {
		localInstance = this;
	}

	public override void OnStartServer () {
		maxLife = 20f;
		maxStam = 10f;
	}

	[Command] public void CmdAddDamage(float val) {damage += val;}
	[SyncVar] private float damage;
	public float Damage {get{return damage;}}

	[Command] public void CmdAddDefense(float val) {defense += val;}
	[SyncVar] private float defense;
	public float Defense {get{return defense;}}

	[Command] public void CmdAddMaxLife(float val) {maxLife += val;}
	[SyncVar] private float maxLife;
	public float MaxLife {get{return maxLife;}}

	[Command] public void CmdAddMaxStam(float val) {maxStam += val;}
	[SyncVar] private float maxStam;
	public float MaxStam {get{return maxStam;}}

	private float baseSpeed;
	public float speedMult = 1f;
	public float Speed {
		get {return baseSpeed*speedMult;}
		set {baseSpeed = value;}
	}
}
