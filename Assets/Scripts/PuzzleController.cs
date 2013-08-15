using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PuzzleController : MonoBehaviour {

    /* class members */
    private static PuzzleController instance;

    public static PuzzleController Get() {
        if (instance == null) {
            instance = FindObjectOfType(typeof(PuzzleController)) as PuzzleController;
        }
        return instance;
    }

    /* instance members */
    public string[] puzzles;
    private int currentPuzzle;
    private List<PuzzleListener> listeners;

    void Start() {
        listeners = new List<PuzzleListener>();
        currentPuzzle = 0;
        LoadNextPuzzle();
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
        currentPuzzle += 1;
        LoadNextPuzzle();
    }

    private void LoadNextPuzzle() {
        Application.LoadLevelAdditive(puzzles[currentPuzzle]);
    }



}
