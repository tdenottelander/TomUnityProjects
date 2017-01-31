using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour {

	public Text distanceText;

	public void UpdateDistanceText(int distance){
		distanceText.text = "Distance: " + distance.ToString () + " m";
	}
}
