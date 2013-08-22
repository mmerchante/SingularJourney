using UnityEngine;
using System.Collections;

public class RandomizeTextureOffset : MonoBehaviour {

    public string[] textures = { "_NoiseTex" };

	void Start () 
    {
        if(this.renderer)
            foreach (string t in textures)
            {
                this.renderer.material.SetTextureOffset(t, new Vector2(Random.value, Random.value));
            }
	}
}
