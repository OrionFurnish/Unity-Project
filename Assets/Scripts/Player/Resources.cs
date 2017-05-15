using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Resources : MonoBehaviour {
	private static float gold;
	public static float Gold {
		get {return gold;}
		set {
			gold = value;
			goldText.text = "Gold: " + gold;
		}
	}

	public static Text goldText;

	void Awake() {
		goldText = GetComponent<Text>();
		Gold += 500;
	}
}
