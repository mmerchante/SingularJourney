using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {


    public GameObject crosshair;

    private bool itemHeld = false;
    private GameObject heldObject;
    private DampedOscillator attractor;

    private Camera MainCam;
    private SelectableObjectEffects lastHovered;


    private int layerMask = ~(1 << 9);


    public void SetOVRCameraController(ref OVRCameraController cameraController) {
        cameraController.GetCamera(ref MainCam);
    }

    void Update() {
        RaycastHit hit;
        SelectableObjectEffects selectable = null;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500f, layerMask)) {
            if (hit.collider.gameObject) {
                crosshair.transform.position = hit.point;
                crosshair.transform.LookAt(MainCam.transform.position);

                selectable = hit.collider.gameObject.GetComponent<SelectableObjectEffects>();
                if (selectable != null && selectable != lastHovered) {
                    selectable.OnHover();
                }
                if (Input.GetMouseButtonDown(0) && selectable != null) {
                    if (!itemHeld) {
                        itemHeld = true;
                        heldObject = selectable.gameObject;
                        selectable.OnSelect();
                        attractor = heldObject.AddComponent<DampedOscillator>();
                    } else {
                        Destroy(attractor);
                        heldObject.GetComponent<SelectableObjectEffects>().OnUnselect();
                        heldObject = null;
                        attractor = null;
                        itemHeld = false;
                    }
                }

            }
        } else {
            crosshair.transform.position = Vector3.down * 1000f;
        }

        if (selectable == null && lastHovered) {
            lastHovered.OnUnhover();
            lastHovered = null;
        }

        if (itemHeld) {
            Transform t = Camera.main.transform;
            Vector3 handLocation = t.position + t.forward * 2.454569f;
            attractor.SetTarget(handLocation);
        }
    }


    public GameObject GetHeldObject() {
        return heldObject;
    }


}
