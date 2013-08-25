using UnityEngine;
using System.Collections;

public class PuzzleEndTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            PuzzleController.Get().OnPuzzleSolved();
            Destroy(gameObject);
        }
    }
}
