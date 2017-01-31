using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float turningSpeed;
	public float sensitivity = 2.5f;
	public Text sensitivityText;
	private float desireableRotationZ;
	private float desireableRotationX;
	private float smoothDampVarZ;
	private float smoothDampVarX;
	public float smoothingTime = 1f;
	Rigidbody rigidBody;

	void Awake(){
		rigidBody = GetComponent<Rigidbody> ();
	}

	void Start(){
		desireableRotationX = transform.rotation.x;
		desireableRotationZ = transform.rotation.z;
		UpdateSensitivityText ();
	}

	void Update () {

		//rigidBody.AddForce (transform.forward * speed);
		transform.position += transform.forward * speed * Time.deltaTime;


		Quaternion rotation = transform.rotation;
		rotation.z = Mathf.SmoothDampAngle (rotation.z, desireableRotationZ, ref smoothDampVarZ, smoothingTime);
		rotation.x = Mathf.SmoothDampAngle (rotation.x, desireableRotationX, ref smoothDampVarX, smoothingTime);
		transform.rotation = rotation;

		float inputX;
		#if UNITY_EDITOR
		inputX = Input.GetAxis("Horizontal");
		#else
		inputX = Input.acceleration.x * sensitivity;
		#endif
		transform.Rotate (new Vector3 (0f, inputX * turningSpeed * Time.deltaTime, 0f));

	}

	public void SetSensitivity(float newSensitivity){
		sensitivity = newSensitivity;
		UpdateSensitivityText ();
	}

	public void IncreaseSensitivity(float increase){
		sensitivity += increase;
		UpdateSensitivityText ();
	}

	void UpdateSensitivityText(){
		sensitivityText.text = sensitivity.ToString();
	}
}
