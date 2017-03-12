﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour {
	private static List<GameObject> possibleTargets;
	private GameObject target;
	private Controller control;

	void Start() {
		if(possibleTargets == null) {possibleTargets = new List<GameObject>();}
		possibleTargets.Add (gameObject);
		control = GetComponent<Controller>();
	}

	void Update() {
		if(target != null) {control.TurnTowards(target.transform.position);}
	}

	public bool hasTarget() {
		return target != null;
	}

	public void SetTarget() {
		if(target != null) {
			target = null;
			return;
		} GameObject closest = null;
		float closestDist = 0f;
		foreach (GameObject possibleTarget in possibleTargets) {
			if (possibleTarget.tag == "Enemy" && possibleTarget != gameObject) {
				float dist = Vector3.Distance(possibleTarget.transform.position, transform.position);
				if (closest == null || dist < closestDist) {
					closest = possibleTarget;
					closestDist = dist;
				}
			}
		} target = closest;
	}
}
