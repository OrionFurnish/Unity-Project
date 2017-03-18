using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBars : NetworkBehaviour {
	public GameObject barsPrefab;
	public GameObject barsTarget;
	BarControl[] bars;
	Stats stats;
	[SyncVar] float curLife;
	float prevLife;
	[SyncVar] float curStam;
	bool draining = false;
	public bool dead = false;

	void Start() {
		GameObject barsObj = Instantiate(barsPrefab, GameObject.Find("Bar Parent").transform);
		barsObj.GetComponent<UIFollowObject>().target = barsTarget;
		bars = barsObj.GetComponentsInChildren<BarControl>();
		stats = GetComponent<Stats>();
		stats.MaxLife = 20f;
		stats.MaxStam = 10f;
		curLife = stats.MaxLife;
		curStam = stats.MaxStam;
	}

	void Update() {
		if(prevLife != curLife) {
			if(curLife <= 0) {
				curLife = 0;
				dead = true;
			} prevLife = curLife;
		} if(!draining) {
			curStam += 2.5f*Time.deltaTime;
			if(curStam > stats.MaxStam) {curStam = stats.MaxStam;}
		} UpdateBars();
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
	[Command] public void CmdTakeDamage(float damage) {curLife -= damage*(1f-stats.Defense/100f);}
	[Command] public void CmdDrainStamina(float amount) {StartCoroutine(DrainStamina(amount, .25f));}
	private void UpdateBars() {
		if(curLife < 0) {bars[0].UpdateBar(stats.MaxLife, 0);}
		else {bars[0].UpdateBar(stats.MaxLife, curLife);}
		if(curStam < 0) {bars[1].UpdateBar(stats.MaxStam, 0);}
		else {bars[1].UpdateBar(stats.MaxStam, curStam);}
	}
}
