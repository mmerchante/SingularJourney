using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour {


    public GameObject crosshairPrefab;

    private GameObject crosshair;

    private bool itemHeld = false;
    private GameObject heldObject;
    private DampedOscillator attractor;

    private Camera MainCam;
    private SelectableObjectEffects lastHovered;


    private int layerMask = ~(1 << 9);


    void Start() {
        crosshair = Instantiate(crosshairPrefab) as GameObject;
    }

    public void SetOVRCameraController(ref OVRCameraController cameraController) {
        cameraController.GetCamera(ref MainCam);
    }

    void Update() {
        RaycastHit hit;
        SelectableObjectEffects selectable = null;
        GameObject hoveringOverThis = null;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 500f, layerMask)) {
            hoveringOverThis = hit.collider.gameObject;


            if (hoveringOverThis != null) {
                float dist = Vector3.Distance(hit.point, MainCam.transform.position);
                crosshair.transform.position = hit.point;
                float scale = (1f + dist*Mathf.Sqrt(2))*0.05f;
                crosshair.transform.localScale = new Vector3(scale, scale, scale);
                crosshair.transform.LookAt(MainCam.transform.position);

                selectable = hoveringOverThis.GetComponent<SelectableObjectEffects>();
                if (selectable != null && selectable != lastHovered) {
                    selectable.OnHover();
                    lastHovered = selectable;
                }
            }
        } else {
            crosshair.transform.position = Vector3.down * 1000f;
        }


        if (Input.GetMouseButtonDown(0)) {
            if (!itemHeld) {
                if (hoveringOverThis != null && (hoveringOverThis.tag == "Pickable" || hoveringOverThis.tag == "TimeWarp")) {
                    heldObject = hoveringOverThis;
                    itemHeld = true;
                    attractor = heldObject.AddComponent<DampedOscillator>();
                } else {
                    Debug.Log(hoveringOverThis.tag);
                }
                if (selectable != null) {
                    selectable.OnSelect();
                }
            } else {
                Destroy(attractor);
                SelectableObjectEffects oldSelected = heldObject.GetComponent<SelectableObjectEffects>();
                if (oldSelected != null) {
                    oldSelected.OnUnselect();
                }
                heldObject = null;
                attractor = null;
                itemHeld = false;
            }
        }

        if (selectable == null && lastHovered != null) {
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
