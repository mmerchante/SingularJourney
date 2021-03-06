using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
public class WarpTimeBody : MonoBehaviour {

    private GameObject center;
    public float startHeight = 10;
    private float endHeight = 0;
    private float maxDrag = 20;
    private Vector3 specificAutoTorque;

    void Start() {
        center = GameObject.FindGameObjectWithTag("TimeWarp");
        transform.rotation = Random.rotation;

        specificAutoTorque = Random.onUnitSphere * 1f;

    }

    void Update() {
        float distance = Vector3.Distance(transform.position, center.transform.position);
        float factor = 1 - Mathf.Exp(-Mathf.Abs(Mathf.Pow(distance, 3.5f) / 500));
        rigidbody.drag = 1 / factor;
        if (rigidbody.drag > maxDrag) {
            rigidbody.drag = maxDrag;
        }


        rigidbody.AddTorque(specificAutoTorque);

        rigidbody.angularDrag = 1f / factor;

    }

    void OnTriggerEnter(Collider otherCollider) {
        GameObject other = otherCollider.gameObject;
        while (other.transform.parent != null) {
            if (other.tag == "Floor") {
                Respawn();
                return;
            }
            other = other.transform.parent.gameObject;
        }
    }

    void OnCollisionEnter(Collision collision) {
        OnTriggerEnter(collision.collider);
    }

    void Respawn() {
        transform.position += Vector3.up * startHeight;
        rigidbody.velocity = Vector3.zero;
    }

}
