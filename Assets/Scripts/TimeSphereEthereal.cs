using UnityEngine;
using System.Collections;

public class TimeSphereEthereal : MonoBehaviour {


    private PickupController pickupController;

    void Start() {
        pickupController = FindObjectOfType(typeof(PickupController)) as PickupController;
    }

    void Update() {
        GameObject held = pickupController.GetHeldObject();
        collider.enabled = (held == null || held.tag != "TimeWarp");
    }
}
