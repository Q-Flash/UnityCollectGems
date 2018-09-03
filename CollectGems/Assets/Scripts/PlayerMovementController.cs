using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
	static Animator anim;

	public float inputDelay = 0.1f;
	public float forwardVel = 12;
	public float rotateVel = 100;
	Quaternion targetRotation;
	Rigidbody rbody;
	float forwardInput, turnInput;

	public Quaternion TargetRotation{
		get { return targetRotation; }
	}

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		targetRotation = transform.rotation;
		if (GetComponent<Rigidbody> ())
			rbody = GetComponent<Rigidbody> ();
		else
			Debug.LogError ("Object does not have a rigid body");
		forwardInput = turnInput = 0;
	}
	
	// Update is called once per frame
	void Update () {
		GetInput ();
		Turn ();
	}

	void FixedUpdate(){
		Run ();
	}


	void GetInput(){
		forwardInput = Input.GetAxis ("Vertical");
		turnInput = Input.GetAxis ("Horizontal");
	}

	void Run(){
		if (Mathf.Abs (forwardInput) > inputDelay) {
			//then move
			anim.SetBool ("isRunning",true);
			rbody.velocity = transform.forward * forwardInput * forwardVel;

		} else {
			//Don't move
			anim.SetBool ("isRunning",false);
			rbody.velocity = Vector3.zero;

		}
			
	}

	void Turn(){
		targetRotation *= Quaternion.AngleAxis (rotateVel * turnInput * Time.deltaTime, Vector3.up);
		transform.rotation = targetRotation;
	}
}
