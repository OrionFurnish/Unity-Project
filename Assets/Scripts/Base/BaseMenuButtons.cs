using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMenuButtons : MonoBehaviour {
	[HideInInspector] public BaseMenu menu;
	public AudioClip buttonClip;
	private AudioSource audioSource;

	void Start() {
		audioSource = GetComponent<AudioSource> ();
	}

	public void BuyWorker() {
		menu.CreateWorker();
		audioSource.PlayOneShot(buttonClip);
	}

	public void BuySoldier() {
		menu.CreateSoldier();
		audioSource.PlayOneShot(buttonClip);
	}
}
