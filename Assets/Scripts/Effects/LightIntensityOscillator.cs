using UnityEngine;
using System.Collections;

public class LightIntensityOscillator : MonoBehaviour {

    public float frequency = 1f;
    public float amplitude = 1f;

    public bool randomOffset = true;

    private float baseIntensity = 0f;
    private float offset = 0f;

    void Start()
    {
        this.baseIntensity = light ? light.intensity : 0f;
        this.offset = Random.value * 2f * Mathf.PI;
    }

	void Update () {
        if (this.light)
            this.light.intensity = baseIntensity + Mathf.Sin(frequency * Time.time + offset) * amplitude;
	}
}
