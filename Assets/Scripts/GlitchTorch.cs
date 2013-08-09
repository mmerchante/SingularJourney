using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlitchTorch : MonoBehaviour {

    public float ampThreshold = .1f;
    public float amplitudeMultiplier = .1f;
    public AnimationCurve amplitudeCurve;

    public float length = 1f;

    public Light glitchLight;
    public float intensityMultiplier = 4f;

    public bool randomTimeOffset = false;

    private List<Material> glitchMaterials = new List<Material>();

    private float intensity = 0f;
    private float timeOffset = 0f;
    
	void Start ()
    {
        if (this.renderer)
        {
            foreach (Material m in this.renderer.materials)
                if (m.HasProperty("_DisplVector"))
                    this.glitchMaterials.Add(m);
        }

        if(randomTimeOffset)
            this.timeOffset = Random.value * length;
	}

	void Update () 
    {
        // We asume the curve is normalized to the 0-1 time range
        float t = Mathf.Repeat(Time.time + timeOffset, length) / length;

        float maxAmplitude = amplitudeCurve.Evaluate(t) * amplitudeMultiplier;

        float glitchIntensity = Random.value * maxAmplitude;

        if (glitchIntensity > ampThreshold && this.rigidbody)
        {
            this.rigidbody.velocity += Random.onUnitSphere * Random.value;
        }

        if (glitchIntensity > ampThreshold)
            intensity = Mathf.MoveTowards(intensity, glitchIntensity * intensityMultiplier, .1f);
        else 
            intensity = Mathf.MoveTowards(intensity, 0f, .01f);

        if (glitchLight)
            glitchLight.intensity = intensity;
       


        foreach (Material m in glitchMaterials)
        {
            float amplitude = Random.value * maxAmplitude;
            Vector3 displVector = Random.onUnitSphere * amplitude;

            m.SetColor("_DisplVector", new Color(displVector.x, displVector.y, displVector.z, 1f));
            m.SetFloat("_Intensity", intensity);
        }
	}
}
