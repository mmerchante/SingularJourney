using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {
    public float pickupDistance = 3.0f;

    private GameObject heldObject;
    private bool itemHeld = false;
    private float objectDist = -0.1f;
    private RaycastHit hit;

    void Start() {
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("DOWN!");
            if (!itemHeld) {
                Debug.Log("not itemheld");
                int layerMask = 1 << 8;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5.0f, layerMask)) {
                    Debug.Log("raycast");
                    if (hit.collider.gameObject) {
                        heldObject = hit.collider.gameObject;
                        heldObject.transform.parent = gameObject.transform;
                        Debug.Log(heldObject.name);
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;

                        Vector3 handLocation = new Vector3(0, objectDist, 0);
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
