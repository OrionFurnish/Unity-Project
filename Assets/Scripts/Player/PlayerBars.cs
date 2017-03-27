using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBars : NetworkBehaviour {
	public static PlayerBars localInstance;
	public bool dead = false;
	public GameObject barsPrefab;
	public GameObject barsTarget;
	BarControl[] bars;
	Stats stats;
	[SyncVar] float curLife;
	[SyncVar] float curStam;
	bool draining = false;

	void Awake() {
		GameObject barsObj = Instantiate(barsPrefab, GameObject.Find("Bar Parent").transform);
		barsObj.GetComponent<UIFollowObject>().target = barsTarget;
		bars = barsObj.GetComponentsInChildren<BarControl>();
		stats = GetComponent<Stats>();

	}

	void Start() {
		if(isServer) {
			curLife = stats.MaxLife;
			curStam = stats.MaxStam;
		}
	}

	public override void OnStartLocalPlayer() {
		localInstance = this;
	}

	void Update() {
		if(isServer && !draining) {curStam += 2.5f*Time.deltaTime;}
		UpdateLife();
		UpdateStam();
	}

	public float GetStamina() {return curStam;}
	[Command] public void CmdTakeDamage(float damage) {curLife -= damage*(1f-stats.Defense/100f);}
	[Command] public void CmdRestoreLife(float amount, float time) {StartCoroutine(RestoreLife(amount, time));}
	[Command] public void CmdDrainStamina(float amount) {StartCoroutine(DrainStamina(amount, .25f));}

	private IEnumerator RestoreLife(float amount, float time) {
		float startTime = Time.time;
		while (Time.time < startTime+time) {
			curLife += amount*Time.deltaTime/time;
			yield return null;
		}
	}

	private IEnumerator DrainStamina(float amount, float time) {
		float startTime = Time.time;
		draining = true;
		while (Time.time < startTime+time) {
			curStam -= amount*Time.deltaTime/time;
			yield return null;
		} draining = false;
	}

	private void UpdateLife() {
		if(curLife > stats.MaxLife) {curLife = stats.MaxLife;}
		else if(curLife < 0) {
			curLife = 0; 
			dead = true;
		} bars[0].UpdateBar(stats.MaxLife, curLife);
	}

	private void UpdateStam() {
		if(curStam <= 0) {bars[1].UpdateBar(stats.MaxStam, 0);}
		else {
			if(curStam > stats.MaxStam) {curStam = stats.MaxStam;}
			bars[1].UpdateBar(stats.MaxStam, curStam);
		}
	}
}
