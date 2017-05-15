using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldMine : MonoBehaviour {
	public float goldWaitTime;
	WorkerAI worker;

	private IEnumerator MineGold() {
		while(worker.gold < worker.maxGold) {
			yield return new WaitForSeconds(goldWaitTime);
			worker.gold += 1;
		} worker.ReturnGold();
		worker = null;
	}

	public void OnTriggerStay(Collider coll) {
		if(coll.gameObject.name == "Peon(Clone)" && worker == null) {
			worker = coll.gameObject.GetComponent<WorkerAI>();
			StartCoroutine(MineGold());
		}
	}
}
