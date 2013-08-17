using UnityEngine;
using System.Collections;

public class Puzzle2Trigger : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        PuzzleController.Get().OnPuzzleSolved("timesphere");
        Destroy(gameObject);
    }
}
