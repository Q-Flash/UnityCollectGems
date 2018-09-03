using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public Transform target;
	public float lookSmooth = 0.09f;
	public Vector3 offsetFromTarget = new Vector3(0, -6, -8);
	public float xtilt = 10;

	Vector3 destination = Vector3.zero;
	PlayerMovementController charController;
	float rotateVel = 0;


	// Use this for initialization
	void Start () {
		SetCameraTarget (target);
	}
	
	// Update is called once per frame
	void LateUpdate () {
		MoveToTarget ();
		LookAtTarget ();
	}

	void SetCameraTarget(Transform t){
		target = t;

		if (target != null) {
			if (target.GetComponent<PlayerMovementController> ()) {
				charController = target.GetComponent<PlayerMovementController> ();
			}

		} else {
			Debug.LogError ("Your camera needs a target");
		}
	}

	void MoveToTarget(){
		destination = charController.TargetRotation * offsetFromTarget;
		destination += target.position;
		transform.position = destination;
	}

	void LookAtTarget(){
		float eulerYAngle = Mathf.SmoothDampAngle (transform.eulerAngles.y, target.eulerAngles.y, ref rotateVel, lookSmooth);
		transform.rotation = Quaternion.Euler (transform.eulerAngles.x, eulerYAngle, 0);
	}
}
