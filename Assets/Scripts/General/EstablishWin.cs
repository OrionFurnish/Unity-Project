using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EstablishWin : MonoBehaviour {
	public static GameObject team1Base;
	public static GameObject team2Base;
	public GameObject endText;
	private bool started = false;

	void Update () {
		if (team1Base != null && team2Base != null) {
			started = true;
		} if(started) {
			if (team1Base == null) {
				endText.SetActive (true);
				endText.GetComponent<Text> ().text = "You Lose!";
			} else if (team2Base == null) {
				endText.SetActive (true);
				endText.GetComponent<Text> ().text = "You Win!";
			}
		}
	}
}
