using UnityEngine;
using System.Collections;

public class Puzzle1Trigger : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        PuzzleController.Get().OnPuzzleSolved("spawner");
        Destroy(gameObject);
    }
}
