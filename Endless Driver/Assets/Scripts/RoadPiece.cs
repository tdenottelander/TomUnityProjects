using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadPiece : MonoBehaviour {

	public string pieceName;
	public Transform nextPieceLocation;
	public RoadMaker roadMaker;
	public bool entryPiece;
	private Vector3 goalPosition;
	private Vector3 smoothDampVar;
	public float smoothingTime = 1f;

	void Start(){
		goalPosition = transform.position;
		if(!entryPiece)
			transform.position += new Vector3 (0f, -100f, 0f);
	}

	void Update(){
		transform.position = Vector3.SmoothDamp (transform.position, goalPosition, ref smoothDampVar, smoothingTime);
	}
}
