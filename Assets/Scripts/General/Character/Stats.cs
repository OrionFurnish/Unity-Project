using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stats: NetworkBehaviour {
	[SerializeField] float startLife;
	[SerializeField] float startStam;
	[SerializeField] float startDam;
	public override void OnStartServer () {
		maxLife = startLife;
		maxStam = startStam;
		damage = startDam;
	}

	[Command] public void CmdAddDamage(float val) {damage += val;}
	[SyncVar] protected float damage;
	public float Damage {get{return damage;}}

	[Command] public void CmdAddDefense(float val) {defense += val;}
	[SyncVar] protected float defense;
	public float Defense {get{return defense;}}

	[Command] public void CmdAddMaxLife(float val) {maxLife += val;}
	[SyncVar] protected float maxLife;
	public float MaxLife {get{return maxLife;}}

	[Command] public void CmdAddMaxStam(float val) {maxStam += val;}
	[SyncVar] protected float maxStam;
	public float MaxStam {get{return maxStam;}}

	protected float baseSpeed;
	public float speedMult = 1f;
	public float Speed {
		get {return baseSpeed*speedMult;}
		set {baseSpeed = value;}
	}
}
