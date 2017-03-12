using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkTransform : NetworkBehaviour {
	[SyncVar] private Vector3 syncPos;
	[SyncVar] private float syncRot;
	[SerializeField] private float posLerpRate;
	[SerializeField] private float rotLerpRate;
	[SerializeField] private float posThresh;
	[SerializeField] private float rotThresh;
	private Vector2 lastPos;
	private float lastRot;
	private Controller control;

	void Start() {
		lastPos = transform.position;
		lastRot = transform.localEulerAngles.z;
		control = GetComponent<Controller> ();
	}

	void Update() {LerpInfo();}
	void FixedUpdate() {TransmitInfo();}

	void LerpInfo() {
		if(!hasAuthority) {
			float lerpDis = Vector3.Distance(transform.position, syncPos);
			if (lerpDis > .05f) {control.SetMoving(true);} 
			else {control.SetMoving (false);}
			if(Time.deltaTime*posLerpRate < 1) {transform.position = Vector3.Lerp(transform.position, syncPos, Time.deltaTime*posLerpRate);}
			else {transform.position = syncPos;}
			if(Time.deltaTime*rotLerpRate < 1) {transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, syncRot, 0)), Time.deltaTime*rotLerpRate);}
			else {transform.rotation = Quaternion.Euler(new Vector3(0, syncRot, 0));}
		}
	}

	[Command] void CmdPosToServer(Vector3 localPos) {syncPos = localPos;}
	[Command] void CmdRotToServer(float localRot) {syncRot = localRot;}
	[Client] void TransmitInfo() {
		if (hasAuthority) {
			if (Vector3.Distance (transform.position, lastPos) > posThresh) {
				CmdPosToServer (transform.position);
				lastPos = transform.position;
			}
			if (IfBeyondThreshold (transform.localEulerAngles.y, lastRot)) {
				lastRot = transform.localEulerAngles.y;
				CmdRotToServer (lastRot);
			}
		}
	}

	bool IfBeyondThreshold(float rot1, float rot2) {return Mathf.Abs(rot1-rot2) > rotThresh;}
}
