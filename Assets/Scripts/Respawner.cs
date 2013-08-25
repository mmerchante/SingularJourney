using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

    public GameObject waypoint;

    void OnTriggerEnter(Collider otherCollider) {
        GameObject other = otherCollider.gameObject;
        if (other != null && other.tag == "Player") {
            other.transform.position = waypoint.transform.position;
        }
    }

}
