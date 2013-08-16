using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {

    private bool itemHeld = false;
    private GameObject heldObject;
    private DampedOscillator oscillator;
    private RaycastHit hit;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!itemHeld) {
                int layerMask = 1 << 8;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5.0f, layerMask)) {
                    if (hit.collider.gameObject) {
                        itemHeld = true;
                        heldObject = hit.collider.gameObject;
                        oscillator = heldObject.AddComponent<DampedOscillator>();
                    }
                }
            } else {
                Destroy(oscillator);
                heldObject = null;
                oscillator = null;
                itemHeld = false;
            }
        }

        if (itemHeld) {
            Transform t = Camera.main.transform;
            Vector3 handLocation = t.position + t.forward * 2.454569f;
            oscillator.SetTarget(handLocation);
        }
    }



}
