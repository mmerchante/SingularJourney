using UnityEngine;
using System.Collections;

public class LinkMaterialToLight : MonoBehaviour {

    public string[] colorProperties = { "_Color" };
    public Light linkedLight;

	void Start () {
        if (this.renderer && this.renderer.material && linkedLight)
            foreach (string c in colorProperties)
                this.renderer.material.SetColor(c, linkedLight.color);
	}
	
}
