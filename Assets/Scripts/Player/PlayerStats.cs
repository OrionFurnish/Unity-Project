using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {
	public GameObject barsPrefab;
	public GameObject barsTarget;
	public float maxLife;
	public bool dead = false;
	[SyncVar] float curLife;
	float prevLife;
	GameObject barsObj;
	BarControl[] bars;

	void Start() {
		barsObj = Instantiate(barsPrefab, GameObject.Find("Bar Parent").transform);
		barsObj.GetComponent<UIFollowObject>().target = barsTarget;
		bars = barsObj.GetComponentsInChildren<BarControl>();
		curLife = maxLife;
	}

	void Update() {
		if(prevLife != curLife) {
			if(curLife <= 0) {
				curLife = 0;
				dead = true;
			} UpdateBar(0);
			prevLife = curLife;
		}
	}

	public void TakeDamage(float damage) {
		if (isServer) {
			curLife -= damage;
		} else if(!isLocalPlayer) {
			CmdTakeDamage (damage);
		}
	}

	[Command] public void CmdTakeDamage(float damage) {
		curLife -= damage;
	}

	private void UpdateBar(int bar) {
		bars[bar].UpdateBar(maxLife, curLife);
	}
}
