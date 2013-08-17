using UnityEngine;
using System.Collections;

public class EntangledObject : MonoBehaviour {

    public GameObject miniature;

    private Vector3 start;
    private Vector3 miniStart;
    private float scaleFactor;

    void Awake() {
        start = transform.position;
        miniStart = miniature.transform.position;
        scaleFactor = transform.localScale.x / miniature.transform.localScale.x;
    }

    void Update() {
        Vector3 current = miniature.transform.position;
        Vector3 delta = current - miniStart;
        transform.position = start + delta * scaleFactor;
        transform.rotation = miniature.transform.rotation;
    }

}
