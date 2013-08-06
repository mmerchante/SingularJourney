using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WarpTimeBody : MonoBehaviour {

    private GameObject center;

    void Start () {
        center = GameObject.FindGameObjectWithTag("TimeWarp");
        transform.rotation = Random.rotation;

    }
    
    void Update () {
        float distance = Vector3.Distance(transform.position, center.transform.position);
        float factor = 1 - Mathf.Exp(-Mathf.Abs(Mathf.Pow(distance, 3.5f) / 500));
        rigidbody.drag = 1 / factor;
        if (transform.position.y < 0f) {
            transform.position += Vector3.up * 10;
            rigidbody.velocity = Vector3.zero;
        }

    }
}
