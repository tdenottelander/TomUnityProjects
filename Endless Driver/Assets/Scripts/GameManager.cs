using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;
	public UI ui;
	public RoadMaker roadMaker;
	public Transform playerSpawnPoint;
	public PlayerController player;
	public float difficultyTimer;
	public float increaseInSpeed;
	public CameraController camController;
	public Canvas GameOverCanvas;
	public float distance = 0;
	private float startingTime;

	public SceneProperties[] scenes;
	private int currentScene = 0;
	public Material roadMaterial;
	public Material skyBoxMaterial;

	[Serializable]
	public struct SceneProperties{
		public int startsAt;
		public Color roadColor;
		public Color backgroundColor;
	}

	void Awake(){
		Instance = this;
	}

	void Start () {
		StartCoroutine (IncreasingSpeed ());
		Restart ();
		Screen.orientation = ScreenOrientation.Portrait;
	}

	void Update () {
		UpdateScore ();
	}

	IEnumerator IncreasingSpeed(){
		while (true) {
			yield return new WaitForSeconds (difficultyTimer);
			Time.timeScale = Time.timeScale + increaseInSpeed;
			Debug.Log ("Level up! Timescale is now " + Time.timeScale);
		}
	}

	public void GameOver(){
		StopAllCoroutines ();
		camController.freezeCamera (true);
		GameOverCanvas.gameObject.SetActive (true);
		Time.timeScale = 0f;
	}

	public void Restart(){
		player.GetComponent<Rigidbody> ().isKinematic = true;
		player.GetComponent<Rigidbody> ().isKinematic = false;
		startingTime = Time.time;
		distance = 0;
		roadMaker.Restart ();
		Time.timeScale = 1f;
		GameOverCanvas.gameObject.SetActive(false);
		player.transform.position = playerSpawnPoint.position;
		player.transform.rotation = playerSpawnPoint.rotation;
		camController.Restart ();
		camController.freezeCamera (false);
	}

	public void UpdateScore(){
		int currentDistance = (int)((Time.time - startingTime) * 7f);
		ui.UpdateDistanceText (currentDistance);
		CheckSceneChange (currentDistance);
	}

	void CheckSceneChange(int currentDistance){
		for (int i = 0; i < scenes.Length; i++) {
			if (currentDistance >= scenes [i].startsAt && currentScene < (i + 1)) {
				ChangeScene (i);
			}
		}
			
	}

	void ChangeScene(int scene){
		RenderSettings.skybox.SetColor("_Tint", scenes [scene].backgroundColor);
		skyBoxMaterial.SetColor("_Colorsrgba", scenes [scene].backgroundColor);
		roadMaterial.color = scenes [scene].roadColor;
	}

	void SaveHighScore(int score){
		if (PlayerPrefs.HasKey ("highScore")) {
			int highScore = PlayerPrefs.GetInt ("highScore");
			if (score > highScore) {
				PlayerPrefs.SetInt ("highScore", score);
			}
		} else {
			PlayerPrefs.SetInt ("highScore", score);
		}
	}
}
