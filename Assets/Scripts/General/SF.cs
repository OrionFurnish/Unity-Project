﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SF : MonoBehaviour {
	public static void MoveTowards(Rigidbody rb, Vector3 targetPos, float speed) {rb.AddForce(GetDirectionForce(rb.gameObject.transform.position, targetPos)*speed);}
	public static Vector3 GetDirectionForce(Vector3 sPos, Vector3 targetPos) {
		float xDif = targetPos.x-sPos.x;
		float zDif = targetPos.z-sPos.z;
		float total = Mathf.Abs(xDif)+Mathf.Abs(zDif);
		return new Vector3(xDif/total, 0, zDif/total);
	}

	public static bool MouseRaycast(out RaycastHit hit, int layerMask) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out hit, 100, layerMask);
	}

	public static bool GetWithinRange(Vector3 pos1, Vector3 pos2, float range) {return Vector3.Distance(pos1, pos2) <= range;}

	public static float NormalizeDegrees(float deg) {
		while(deg >= 360) {deg -= 360;}
		while(deg < 0) {deg += 360;}
		return deg;
	}

	public static float OffsetToDegrees(Vector3 offset) {return NormalizeDegrees(Mathf.Atan2(offset.y, offset.x)*Mathf.Rad2Deg);}
	public static Vector3 DegreesToOffset(float deg, float rad) {
		float offX = rad*Mathf.Cos(Mathf.Deg2Rad*deg);
		float offY = rad*Mathf.Sin(Mathf.Deg2Rad*deg);
		return new Vector3(offX, offY, 0);
	}
}