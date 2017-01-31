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
	public Rigidbody rb;

	void Awake(){
		rb = GetComponent<Rigidbody> ();
	}

	void Start(){
		desireableRotationX = transform.rotation.x;
		desireableRotationZ = transform.rotation.z;
		UpdateSensitivityText ();
	}

	void FixedUpdate () {


		rb.MovePosition (transform.position + transform.forward * speed * Time.deltaTime);
		//rigidBody.AddForce (transform.forward * speed);
		//transform.position += transform.forward * speed * Time.deltaTime;



		Quaternion rotation = transform.rotation;
		Vector3 rotVec = new Vector3 ();
		rotVec.z = Mathf.SmoothDampAngle (rotation.eulerAngles.z, desireableRotationZ, ref smoothDampVarZ, smoothingTime);
		rotVec.x = Mathf.SmoothDampAngle (rotation.eulerAngles.x, desireableRotationX, ref smoothDampVarX, smoothingTime);
		rotVec.y = Input.GetAxis("Horizontal") + (Input.acceleration.x * sensitivity);
		rotation.eulerAngles = rotVec;
		//transform.rotation = rotation;
		rb.MoveRotation(Quaternion.Euler(rotVec));

		//float inputX = Input.GetAxis("Horizontal") + (Input.acceleration.x * sensitivity);
		//transform.Rotate (new Vector3 (0f, inputX * turningSpeed * Time.deltaTime, 0f));

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
