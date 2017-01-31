using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextPiece : MonoBehaviour {

	public RoadPiece roadPiece;

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
//			Debug.Log ("Trigger");
			roadPiece.roadMaker.MakeNewPiece ();
		}
	}
}
