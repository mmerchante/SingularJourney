using UnityEngine;
using System.Collections;

public class OrientToCamera : MonoBehaviour {

    // Simple billboard
	void Update () {
        Camera cam = Camera.mainCamera;
        this.transform.LookAt(cam.transform.position, cam.transform.up);
	}
}
