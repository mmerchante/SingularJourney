using UnityEngine;
using System.Collections;

public class SpeedLimit : MonoBehaviour {


    private float limit = 0.1f;

    void FixedUpdate () {
        float mag = rigidbody.velocity.magnitude;
        if (mag > limit) {
            rigidbody.velocity = limit * rigidbody.velocity.normalized;
        }
    }
}
