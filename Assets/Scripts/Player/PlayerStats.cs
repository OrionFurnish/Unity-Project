using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerStats : NetworkBehaviour {
	public GameObject barsPrefab;
	public GameObject barsTarget;
	public bool dead = false;
	public float maxLife;
	public float maxStam;
	[SyncVar] float curLife;
	[SyncVar] float curStam;
	float prevLife;
	GameObject barsObj;
	BarControl[] bars;
	bool draining = false;

	void Start() {
		barsObj = Instantiate(barsPrefab, GameObject.Find("Bar Parent").transform);
		barsObj.GetComponent<UIFollowObject>().target = barsTarget;
		bars = barsObj.GetComponentsInChildren<BarControl>();
		curLife = maxLife;
		curStam = maxStam;
	}

	void Update() {
		if(prevLife != curLife) {
			if(curLife <= 0) {
				curLife = 0;
				dead = true;
			} UpdateBar(0, maxLife, curLife);
			prevLife = curLife;
		} if(!draining) {
			curStam += 2.5f*Time.deltaTime;
			if(curStam > maxStam) {curStam = maxStam;}
		} if(curStam < 0) {UpdateBar(1, maxStam, 0);}
		else {UpdateBar(1, maxStam, curStam);}
	}

	[Command] public void CmdTakeDamage(float damage) {
		curLife -= damage;
	}

	[Command] public void CmdDrainStamina(float amount) {
		//curStam -= amount;
		StartCoroutine(DrainStamina(amount, .25f));
	}

	private IEnumerator DrainStamina(float amount, float time) {
		float startTime = Time.time;
		draining = true;
		while (Time.time < startTime + time) {
			curStam -= amount * Time.deltaTime / time;
			yield return null;
		} draining = false;
	}

	public float GetStamina() {return curStam;}

	private void UpdateBar(int bar, float maxVal, float curVal) {
		bars[bar].UpdateBar(maxVal, curVal);
	}
}
