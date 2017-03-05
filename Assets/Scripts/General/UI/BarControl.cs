using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControl : MonoBehaviour {
	public GameObject barObj;
	public GameObject backObj;

	public void UpdateBar(float maxVal, float curVal) {
		float valRatio = curVal/maxVal;
		Vector3 baseScale = backObj.transform.localScale;
		barObj.transform.localScale = new Vector3(baseScale.x*valRatio, baseScale.y, baseScale.z);
	}
}
