using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public Transform target;
	public Vector3 offset;
	public float rotation;
	bool freeze = false;
	Vector3 SmoothDampVar;
	public float smoothingTime = .1f;
	
	void LateUpdate(){
		if (!freeze) {
			//transform.position = target.position + offset;
			//transform.rotation = Quaternion.Euler (new Vector3 (rotation, 0f, 0f));
			transform.position = Vector3.SmoothDamp(transform.position, target.position, ref SmoothDampVar, smoothingTime);
			transform.rotation = target.rotation;
		}
	}

	public void freezeCamera(bool freeze){
		this.freeze = freeze;
	}

	public void Restart(){
		transform.position = target.position;
		transform.rotation = target.rotation;
	}
}
