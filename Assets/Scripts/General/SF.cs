﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SF : MonoBehaviour {
	public static void MoveTowards(Rigidbody rb, Vector3 targetPos, float speed) {rb.AddForce(GetDirectionForce(rb.gameObject.transform.position, targetPos)*speed);}
	public static void MoveTowards(Rigidbody rb, Vector3 targetPos, float speed, Vector3 relativePos) {rb.AddForce(GetDirectionForce(relativePos, targetPos)*speed);}
	public static Vector3 GetDirectionForce(Vector3 sPos, Vector3 targetPos) {
		float xDif = targetPos.x-sPos.x;
		float zDif = targetPos.z-sPos.z;
		float total = Mathf.Abs(xDif)+Mathf.Abs(zDif);
		return new Vector3(xDif/total, 0, zDif/total);
	}

	public static IEnumerator TurnTowards(Rigidbody rb, Vector3 target, float turnRate) {
		Vector3 relativePos = target - rb.position;
		relativePos.y = 0;
		Quaternion rot = Quaternion.LookRotation(relativePos);
		while(Quaternion.Angle(rb.rotation, rot) != 0) {
			rb.MoveRotation(Quaternion.Lerp(rb.rotation, rot, turnRate*Time.deltaTime));
			yield return null;
		}
	}

	public static bool MouseRaycast(out RaycastHit hit, int layerMask) {
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		return Physics.Raycast(ray, out hit, 100f, layerMask);
	}

	public static bool GetWithinRange(Vector3 pos1, Vector3 pos2, float range) {return Vector3.Distance(pos1, pos2) <= range;}
}
