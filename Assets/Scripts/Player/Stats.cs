using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Stats: NetworkBehaviour {
	[Command] private void CmdUpdateDamage(float newDamage) {damage = newDamage;}
	[SyncVar] private float damage;
	public float Damage {
		get {return damage;}
		set {
			if(isServer) {damage = value;}
			else {CmdUpdateDamage(value);}
		}
	}

	[Command] private void CmdUpdateDefense(float newDefense) {defense = newDefense;}
	[SyncVar] private float defense;
	public float Defense {
		get {return defense;} 
		set {
			if(isServer) {defense = value;}
			else {CmdUpdateDefense(value);}
		}
	}

	[Command] private void CmdUpdateMaxLife(float newLife) {maxLife = newLife;}
	[SyncVar] private float maxLife;
	public float MaxLife {
		get {return maxLife;}
		set {
			if(isServer) {maxLife = value;}
			else {CmdUpdateMaxLife(value);}
		}
	}

	[Command] private void CmdUpdateMaxStam(float newStam) {maxStam = newStam;}
	[SyncVar] private float maxStam;
	public float MaxStam {
		get {return maxStam;}
		set {
			if(isServer) {maxStam = value;}
			else {CmdUpdateMaxStam(value);}
		}
	}

	private float baseSpeed;
	public float speedMult = 1f;
	public float Speed {
		get {return baseSpeed*speedMult;}
		set {baseSpeed = value;}
	}
}
