using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {
    public float pickupDistance = 3.0f;

    private GameObject heldObject;
    private Rigidbody heldRB;
    private bool itemHeld = false;
    private RaycastHit hit;
    public float kFactor = 36f;
    public float cFactor = 8f;

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (!itemHeld) {
                int layerMask = 1 << 8;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 5.0f, layerMask)) {
                    if (hit.collider.gameObject) {
                        /*
                        heldObject.transform.parent = gameObject.transform;
                        heldObject.GetComponent<Rigidbody>().isKinematic = true;
                        heldObject.transform.localPosition = handLocation;*/
                        heldObject = hit.collider.gameObject;
                        heldRB = heldObject.rigidbody;
                        itemHeld = true;
                    }
                }
            } else {
                /*heldObject.transform.parent = null;
                heldObject.GetComponent<Rigidbody>().isKinematic = false;*/
                heldObject = null;
                itemHeld = false;
            }
        }

        if (itemHeld) {
            Transform t = Camera.main.transform;
            Vector3 handLocation = t.position +t.right*0.6081293f + t.up*0.4828247f + t.forward * 2.454569f;
            Vector3 bounceForce = kFactor * (handLocation - heldObject.transform.position);

            heldRB.AddForce(bounceForce);

            // http://hyperphysics.phy-astr.gsu.edu/hbase/oscda2.html#c1
            Vector3 damping = -heldRB.velocity * cFactor;
            heldRB.AddForce(damping);
        }
    }



}
