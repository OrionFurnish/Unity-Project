using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PointerIconCtrl : MonoBehaviour {
	static Image image;
	static bool vis = false;

	void Start() {
		image = GetComponent<Image>();
	}

	void Update() {
		if(vis) {transform.position = Input.mousePosition;}
	}

	public static void Activate(Sprite icon) {
		image.sprite = icon;
		image.color = new Color(1, 1, 1, .9f);
		vis = true;
	}

	public static void Deactivate() {
		image.color = new Color(1, 1, 1, 0);
		vis = false;
	}

	public static bool IsActive() {
		return vis;
	}
}
