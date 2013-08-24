using UnityEngine;
using System.Collections;

public class Oscillator : MonoBehaviour {

    public Vector3 direction = Vector3.one;
    public float frequency = 1f;
    public float amplitude = 1f;

    private Vector3 initialPosition;

    void Start()
    {
        this.initialPosition = this.transform.localPosition;
    }

    void Update () 
    {
        this.transform.localPosition = initialPosition + direction * amplitude * Mathf.Sin(frequency * Time.time);
	}
}
