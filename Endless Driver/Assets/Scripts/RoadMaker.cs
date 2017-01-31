using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadMaker : MonoBehaviour {

	public RoadPiece entryPiece;
	private RoadPiece previousPiece;
	private RoadPiece currentPiece;
	private RoadPiece nextPiece;
	public RoadPiece[] availablePieces;
	public List<RoadPiece> currentPieces;

	void Start () {
	}

	public void MakeNewPiece(){
		if (previousPiece != null) {
			if (!previousPiece.entryPiece) {
				currentPieces.Remove (previousPiece);
				Destroy (previousPiece.gameObject);
			} else {
				previousPiece.gameObject.SetActive (false);
			}
		}

		previousPiece = currentPiece;
		currentPiece = nextPiece;
		nextPiece = Instantiate (availablePieces [Random.Range(0, availablePieces.Length)],
			nextPiece.nextPieceLocation.position, nextPiece.nextPieceLocation.rotation, transform);
		nextPiece.roadMaker = this;
		currentPieces.Add (nextPiece);
//		Debug.Log ("Creating new piece");
	}

	public void Restart(){
		entryPiece.gameObject.SetActive (true);
//		Debug.Log (currentPieces.Count);
		for (int i = 0; i < currentPieces.Count; i++) {
			Debug.Log ("destroy " + currentPieces [i].name);
			Destroy (currentPieces [i].gameObject);
		}
		currentPieces = new List<RoadPiece> ();
		entryPiece.roadMaker = this;
		currentPiece = entryPiece;
		nextPiece = Instantiate (availablePieces [0], currentPiece.nextPieceLocation.position, currentPiece.nextPieceLocation.rotation, transform);
		nextPiece.roadMaker = this;
		currentPieces.Add (nextPiece);
	}
}
