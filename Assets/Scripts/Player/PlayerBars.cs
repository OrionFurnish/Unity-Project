using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerBars : LifeBar {
	public static PlayerBars localInstance;
	public GameObject barsTarget;
	BarControl[] bars;
	Stats stats;
	[SyncVar] float curStam;
	bool draining = false;

	public override void Awake() {
		barObj = Instantiate(barPrefab, GameObject.Find("Bar Parent").transform);
		barObj.GetComponent<UIFollowObject>().target = barsTarget;
		bars = barObj.GetComponentsInChildren<BarControl>();
		stats = GetComponent<Stats>();
	}

	public override void Start() {
		if(isServer) {
			curLife = stats.MaxLife;
			curStam = stats.MaxStam;
		}
	}

	public override void OnStartLocalPlayer() {
		localInstance = this;
	}

	public override void Update() {
		if(isServer && !draining) {curStam += 2.5f*Time.deltaTime;}
		UpdateLife();
		UpdateStam();
	}

	public float GetStamina() {return curStam;}
	[Command] public override void CmdTakeDamage(float damage) {curLife -= damage*(1f-stats.Defense/100f);}
	[Command] public void CmdDrainStamina(float amount) {StartCoroutine(DrainStamina(amount, .25f));}

	private IEnumerator DrainStamina(float amount, float time) {
		float startTime = Time.time;
		draining = true;
		while (Time.time < startTime+time) {
			curStam -= amount*Time.deltaTime/time;
			yield return null;
		} draining = false;
	}

	protected override void UpdateLife() {
		if(curLife > stats.MaxLife) {curLife = stats.MaxLife;}
		else if(curLife < 0) {
			curLife = 0; 
			dead = true;
			NetworkServer.Destroy(gameObject);
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
