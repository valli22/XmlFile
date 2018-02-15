using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float limitUp;
	public float limitDown;
	public float camRotationSpeed;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (1)) {
			float y = Input.GetAxis ("Mouse X");
			float x = Input.GetAxis ("Mouse Y");
			transform.Rotate ( new Vector3(x*camRotationSpeed,-y*camRotationSpeed,0));
			float xRot = Mathf.Clamp ((transform.localEulerAngles.x > 180 ? transform.localEulerAngles.x - 360 : transform.localEulerAngles.x), limitDown, limitUp);
			transform.localEulerAngles = new Vector3 (xRot,transform.localEulerAngles.y,0);
		}

	}
}
