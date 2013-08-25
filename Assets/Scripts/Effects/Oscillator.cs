using UnityEngine;
using System.Collections;

public class Oscillator : MonoBehaviour {

    public Vector3 direction = Vector3.up;
    public float frequency = 1f;
    public float amplitude = 1f;
    public float offset = 0f;

    public bool randomOffset = true;

    private Vector3 initialPosition;

    void Start()
    {
        if (randomOffset)
            this.offset = Random.value * 2f * Mathf.PI;

        this.initialPosition = this.transform.localPosition;
    }

    void Update () 
    {
        this.transform.localPosition = initialPosition + direction * amplitude * Mathf.Sin(frequency * Time.time + offset);
	}
}
