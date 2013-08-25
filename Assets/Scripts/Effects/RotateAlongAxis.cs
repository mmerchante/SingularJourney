using UnityEngine;
using System.Collections;

public class RotateAlongAxis : MonoBehaviour {

    public Vector3 axis = Vector3.up;
    public float speed = 1f;

    public bool randomOffset = true;

    private float offset = 0f;

    private Quaternion originalRot;

    void Start()
    {
        originalRot = this.transform.localRotation;

        if (randomOffset)
            offset = Random.value * 2f * Mathf.PI;
    }

	void Update () {
        this.transform.localRotation = originalRot * Quaternion.Euler(axis * speed * (Time.time + offset));
	}
}
