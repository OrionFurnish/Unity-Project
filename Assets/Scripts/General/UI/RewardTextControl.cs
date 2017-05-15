using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RewardTextControl : MonoBehaviour {
	public float posIncrement;
	public float fadeSpeed;
	Vector3 worldPos;
	Text text;

	void Start () {
		worldPos = Camera.main.ScreenToWorldPoint(transform.position);
		text = GetComponent<Text>();
	}

	void Update() {
		worldPos += new Vector3(0, 0, posIncrement*Time.deltaTime);
		transform.position = Camera.main.WorldToScreenPoint(worldPos);
		text.color -= new Color(0, 0, 0, fadeSpeed*Time.deltaTime);
		if(text.color.a <= 0) {Destroy (gameObject);}
	}
}
