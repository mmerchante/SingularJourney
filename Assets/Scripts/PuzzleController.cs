using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PuzzleController : MonoBehaviour {

    /* class members */
    private static PuzzleController instance;

    private static PuzzleController Get() {
        if (instance == null) {
            instance = FindObjectOfType(typeof(PuzzleController)) as PuzzleController;
        }
        return instance;
    }

    /* instance members */

    private List<PuzzleListener> listeners;

    void Start() {
        listeners = new List<PuzzleListener>();
    }
    
    /**
     * Register to be aware of puzzle solving events
     **/
    internal void AddListener(PuzzleListener listener) {
        listeners.Add(listener);
    }

    /**
     * Called when a puzzle is solved, by a puzzle solving event publisher
     **/
    internal void OnPuzzleSolved(string name) {
        foreach (PuzzleListener listener in listeners) {
            listener.OnPuzzleSolved(name);
        }
    }





}
