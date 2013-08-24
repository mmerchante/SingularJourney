using UnityEngine;
using System.Collections;

public class EntangledObject : MonoBehaviour {

    public GameObject miniature;
    public float factorCorrection = 1.0f;

    private Vector3 start;
    private Vector3 miniStart;
    private float scaleFactor;
    private bool started = false;

    void Start() {
        enabled = false;
    }

    void Update() {
        if (!started) {
            start = transform.position;
            miniStart = miniature.transform.position;
            scaleFactor = factorCorrection * transform.localScale.x / miniature.transform.localScale.x;
            started = true;
        } else {
            Vector3 current = miniature.transform.position;
            Vector3 delta = current - miniStart;
            transform.position = start + delta * scaleFactor;
            transform.rotation = miniature.transform.rotation;
        }
        
    }

}
