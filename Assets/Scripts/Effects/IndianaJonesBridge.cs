using UnityEngine;
using System.Collections;

public class IndianaJonesBridge : MonoBehaviour {

    public Transform target;
    public Transform locator1, locator2;

    void Start()
    {
        if (this.renderer && this.renderer.material)
        {
            this.renderer.material.SetFloat("_Mask", 1f);            
        }
    }

	void Update () 
    {
        if (this.renderer && this.renderer.material && target && locator1 && locator2)
        {
            Vector3 locatorVector = Vector3.Scale(new Vector3(1f, 0f, 1f), locator2.position - locator1.position);

            Vector3 mainDir = locatorVector.normalized;

            float bridgeDistance = locatorVector.magnitude;

            Vector3 dirToMainLocator = Vector3.Scale(new Vector3(1f, 0f, 1f), locator1.position - target.position);

            if (Vector3.Dot(dirToMainLocator.normalized, mainDir) < 0f)
            {
                float magnitude = dirToMainLocator.magnitude;

                float t = Mathf.Clamp01(magnitude / bridgeDistance);

                this.renderer.material.SetFloat("_Mask", 1f - t);
            }

        }
	}
}
