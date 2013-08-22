using UnityEngine;
using System.Collections;

public class RandomizeMaterialIntensity : MonoBehaviour {

    public float maxIntensity = 1.5f;
    public float minIntensity = .1f;

	void Start () 
    {
        float i = Random.RandomRange(minIntensity, maxIntensity);

        if (this.renderer && this.renderer.material.HasProperty("_Intensity"))
            this.renderer.material.SetFloat("_Intensity", i);
	}
}
