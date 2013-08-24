using UnityEngine;
using System.Collections;

public class SandboxItem : MonoBehaviour {

    void Awake() {
        if (Application.loadedLevelName == "MainSceneDeco") {
            enabled = false;
            Destroy(gameObject);
        }
    }

}
