using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LifeBar : NetworkBehaviour {
	public bool dead = false;
	public GameObject barPrefab;
	protected BarControl bar;
	[SerializeField] private float maxLife;
	[SyncVar] protected float curLife;
	protected GameObject barObj;

	public virtual void Awake() {
		barObj = Instantiate(barPrefab, GameObject.Find("Bar Parent").transform);
		barObj.GetComponent<UIFollowObject>().target = gameObject;
		bar = barObj.GetComponentInChildren<BarControl>();
	}

	public GameObject Get() {
		return barObj;
	}

	public virtual void Start() {
		if(isServer) {curLife = maxLife;}
	}

	public virtual void Update() {
		UpdateLife();
	}
		
	[Command] public virtual void CmdTakeDamage(float damage) {curLife -= damage;}
	[Command] public void CmdRestoreLife(float amount, float time) {StartCoroutine(RestoreLife(amount, time));}

	private IEnumerator RestoreLife(float amount, float time) {
		float startTime = Time.time;
		while (Time.time < startTime+time) {
			curLife += amount*Time.deltaTime/time;
			yield return null;
		}
	}

	protected virtual void UpdateLife() {
		if(curLife > maxLife) {curLife = maxLife;}
		else if(curLife < 0) {
			curLife = 0; 
			dead = true;
			NetworkServer.Destroy(gameObject);
		} bar.UpdateBar(maxLife, curLife);
	}
}
