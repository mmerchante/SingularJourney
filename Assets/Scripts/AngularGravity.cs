using UnityEngine;
using System.Collections;

public class AngularGravity : MonoBehaviour {

    private Vector3 gravity;

    private float changeTime = 3f;
    private float time;

    void Start() {
        Randomize();
        time = 0;
    }

    void Update() {
        time += Time.deltaTime;
        if (time > changeTime) {
            time -= changeTime;
            Randomize();
        }
        
        rigidbody.AddTorque(gravity);
    }

    void Randomize() {
        gravity = Random.onUnitSphere * 10f;
    }
}
