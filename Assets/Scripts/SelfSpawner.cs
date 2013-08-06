using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SelfSpawner : MonoBehaviour {

    public float interval = 1f;
    public int maxClones = 5;
    public bool original = true;

    private float passed = 0;
    private GameObject obj;
    private Queue<SelfSpawner> clones;
    private bool fading = false;

    void Start() {
        obj = gameObject;
        clones = new Queue<SelfSpawner>(maxClones);
    }

    void Update() {
        if (original) {
            passed += Time.deltaTime;
            if (passed > interval) {
                passed -= interval;
                DoSpawn();
            }
        } else {
            if (fading) {
                transform.localScale *= 0.9f;
                if (transform.localScale.magnitude < 0.1f) {
                    Destroy(gameObject);
                }
            }
        }
    }

    void DoSpawn() {
        SelfSpawner copy = (GameObject.Instantiate(obj) as GameObject).GetComponent<SelfSpawner>();
        copy.original = false;
        if (clones.Count >= maxClones) {
            SelfSpawner old = clones.Dequeue();
            old.PlanFade();
        }
        clones.Enqueue(copy);
    }

    private void PlanFade() {
        fading = true;
    }
}
