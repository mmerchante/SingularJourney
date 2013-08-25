using UnityEngine;
using System.Collections;

public class RespawnResetter : MonoBehaviour {

    private Vector3 start;

    private float time = 0;

    void Update() {
        time += Time.deltaTime;
        if (time > 0.5f) {
            start = transform.position;
            enabled = false;
        }
    }

    internal void OnRespawn() {
        transform.position = start;
    }

}
