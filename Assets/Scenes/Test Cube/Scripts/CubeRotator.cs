using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeRotator : MonoBehaviour {
    public Camera mainCamera;

	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1)) {
            float dx = Input.GetAxis("MouseX");
            float dy = Input.GetAxis("MouseY");

            Quaternion cameraRot = mainCamera.transform.rotation;
            gameObject.transform.RotateAround(gameObject.transform.position,
                mainCamera.transform.up, -dx);
            gameObject.transform.RotateAround(gameObject.transform.position,
                mainCamera.transform.right, dy);
        }
	}
}
