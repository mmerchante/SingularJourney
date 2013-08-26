using UnityEngine;
using System.Collections;

public class OceanObject : MonoBehaviour {

    public Transform target;

    public void Update()
    {
        if (this.renderer && this.renderer.material && target)
        {
            Vector3 p = transform.position;

            Vector3 dist = target.position - p;
            
            this.renderer.material.SetVector("_Target", new Vector4(dist.x, 0f, dist.z, 0f));
        }
    }
}
