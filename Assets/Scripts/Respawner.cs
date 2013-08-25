using UnityEngine;
using System.Collections;

public class Respawner : MonoBehaviour {

    public GameObject waypoint;

    private OVRMainMenu menu;

    private GameObject player;

    private bool fading = false;

    void Start() {
        menu = FindObjectOfType(typeof(OVRMainMenu)) as OVRMainMenu;
    }

    void Update() {
        if (fading && menu.GetAlpha() == 1f) {
            fading = false;
            player.transform.position = waypoint.transform.position;
            player.transform.rotation = waypoint.transform.rotation;
            menu.Fade(false);
        }
    }

    void OnTriggerEnter(Collider otherCollider) {
        GameObject other = otherCollider.gameObject;
        if (other != null && other.tag == "Player") {
            player = other;
            menu.SetFadeTime(2);
            menu.Fade(true);
            Debug.Log(name);
            fading = true;
        }
    }

}
