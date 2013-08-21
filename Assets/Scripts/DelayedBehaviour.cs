using UnityEngine;
using System.Collections;

public class DelayedBehaviour : MonoBehaviour {

    public MonoBehaviour behaviour;
    public float delay = 0.5f;

    private float time;

    void Start() {
        time = 0f;
    }

    void Update() {
        time += Time.deltaTime;
        if (time > delay) {
            behaviour.enabled = true;
            this.enabled = false;
        }
    }
}
