using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GlitchObject : MonoBehaviour {

    public float amplitudeMultiplier = .1f;
    public AnimationCurve amplitudeCurve;

    private List<Material> glitchMaterials = new List<Material>();
    
	void Start ()
    {
        if (this.renderer)
        {
            foreach (Material m in this.renderer.materials)
                if (m.HasProperty("_DisplVector"))
                    this.glitchMaterials.Add(m);
        }	
	}

	void Update () 
    {
        // We asume the curve is normalized to the 0-1 time range
        float t = Mathf.Repeat(Time.time, 1f);

        float maxAmplitude = amplitudeCurve.Evaluate(t) * amplitudeMultiplier;

        foreach (Material m in glitchMaterials)
        {
            float amplitude = Random.value * maxAmplitude;
            Vector3 displVector = Random.onUnitSphere * amplitude;

            m.SetColor("_DisplVector", new Color(displVector.x, displVector.y, displVector.z, amplitude));
        }
	}
}
