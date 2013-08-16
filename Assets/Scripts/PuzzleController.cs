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
    private AsyncOperation async;

    void Start() {
        listeners = new List<PuzzleListener>();
        OnLevelLoaded(0);
        currentPuzzle = 1;
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
        async = Application.LoadLevelAdditiveAsync(puzzles[currentPuzzle]);
    }

    void Update() {
        if (async != null && async.isDone) {
            async = null;
            OnLevelLoaded(currentPuzzle);
        }
    }

    private void OnLevelLoaded(int level) {
        Debug.Log("Level " + level + " loaded!");
        GameObject spawner = GameObject.Find("Puzzle" + level + "Location");
        GameObject[] all = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];

        int n = 0;
        foreach (GameObject go in all) {
            Marker marker = go.GetComponent<Marker>();
            if (marker == null) {
                if (go.transform.parent == null) {
                    go.transform.position += spawner.transform.position;
                }
                go.AddComponent<Marker>();
                n += 1;
            }
        }
        Debug.Log(n + " objects moved");
    }


}
