using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class WorkerAI : NetworkBehaviour {
	[HideInInspector] public BaseMenu baseMenu;
	public float speed;
	public GameObject goldRewardText;
	public int gold;
	public int maxGold;
	GoldMine goldMine;
	GameObject target;

	void Start() {
		Invoke ("DetermineLocalBase", 0f);
	}

	void DetermineLocalBase() {
		if(hasAuthority) {baseMenu = BaseMenu.localBase.GetComponent<BaseMenu> ();}
		else {baseMenu = BaseMenu.otherBase.GetComponent<BaseMenu> ();}
		goldMine = baseMenu.goldMine.GetComponent<GoldMine>();
		target = goldMine.gameObject;
	}

	void Update() {
		Move();
	}

	void Move() {
		transform.position += SF.GetDirectionForce(transform.position, target.transform.position)*speed*Time.deltaTime;
	}

	public void ReturnGold() {
		target = baseMenu.gameObject;
	}

	public void OnTriggerEnter(Collider coll) {
		if(coll.gameObject == target) {
			if(target == baseMenu.gameObject) {
				target = goldMine.gameObject;
				Text text = Instantiate(goldRewardText, GameObject.Find("Canvas").transform).GetComponent<Text>();
				text.transform.position = Camera.main.WorldToScreenPoint(transform.position);
				text.text = "+" + gold + " Gold";
				Resources.Gold += gold;
				gold = 0;
			}
		}
	}
}
