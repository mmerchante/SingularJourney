using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SelfSpawner : MonoBehaviour {

    public float interval = 1f;
    public int maxClones = 5;
    public bool original = true;
    public GameObject prefab;
    public GameObject holder;

    private float passed = 0;
    private Queue<SelfSpawner> clones;
    private bool fading = false;

    void Start() {
        clones = new Queue<SelfSpawner>(maxClones);
        if (original) {
        } else {
            gameObject.layer = 0; //back to default layer, non pickable
        }
    }

    void Update() {
        if (original) {
            passed += Time.deltaTime;
            if (passed > interval) {
                passed -= interval;
                if (maxClones > 0) {
                    DoSpawn();
                }
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
        SelfSpawner copy = (GameObject.Instantiate(prefab) as GameObject).GetComponent<SelfSpawner>();
        copy.gameObject.AddComponent<Marker>(); // prevent it from moving on level change
        copy.original = false;
        Vector3 r = Random.onUnitSphere*0.01f;
        r.y = 0;
        copy.transform.position = transform.position + r;
        copy.transform.rotation = transform.rotation;
        copy.transform.localScale = transform.localScale;
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
