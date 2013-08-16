using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WarpTimeBody : MonoBehaviour {

    private GameObject center;
    private float startHeight = 10;
    private float endHeight = 0;
    private float maxDrag = 20;

    void Start() {
        center = GameObject.FindGameObjectWithTag("TimeWarp");
        transform.rotation = Random.rotation;

    }

    void Update() {
        float distance = Vector3.Distance(transform.position, center.transform.position);
        float factor = 1 - Mathf.Exp(-Mathf.Abs(Mathf.Pow(distance, 3.5f) / 500));
        rigidbody.drag = 1/factor;
        if (rigidbody.drag > maxDrag) {
            rigidbody.drag = maxDrag;
        }
        rigidbody.angularDrag = 0.05f / factor;

    }

    void OnCollisionEnter(Collision collision) {
        if (collision.other.tag == "Floor") {
            Respawn();
        }
    }

    void Respawn() {
        transform.position += Vector3.up * startHeight;
        rigidbody.velocity = Vector3.zero;
    }

}
