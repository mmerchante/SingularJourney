using UnityEngine;
using System.Collections;

public class RotateAlongAxis : MonoBehaviour {

    public Vector3 axis = Vector3.up;
    public float speed = 1f;

    public bool randomOffset = true;

    private float offset = 0f;

    void Start()
    {
        if (randomOffset)
            offset = Random.value * 2f * Mathf.PI;
    }

	void Update () {
        this.transform.localRotation = Quaternion.Euler(axis * speed * (Time.time + offset));
	}
}
