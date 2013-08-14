using UnityEngine;
using System.Collections;

public class SandboxItem : MonoBehaviour {

    void Awake() {
        if (Application.loadedLevelName == "MainScene") {
            enabled = false;
            Destroy(gameObject);
        }
    }

}
