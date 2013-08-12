using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {
    public float pickupDistance = 3.0f;

    private GameObject heldObject;
    private bool itemHeld = false;
    private RaycastHit hit;

    void Start() {
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!itemHeld) {
                int layerMask = 1 << 8;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5.0f, layerMask)) {
                    if (hit.collider.gameObject) {
                        heldObject = hit.collider.gameObject;
                        heldObject.transform.parent = gameObject.transform;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;

                        Vector3 handLocation = new Vector3(0.6081293f, 0.4828247f, 2.454569f);
                        heldObject.transform.localPosition = handLocation;
                        itemHeld = true;
                    }
                }
            } else {
                heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;
                heldObject = null;
                itemHeld = false;
            }
        }

    }



}
