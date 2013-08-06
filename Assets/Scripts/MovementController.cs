using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour {
    public enum AnimationType {
        Linear,
        Smooth
    }

    public Vector3 infinity;

    private Queue<Vector3> targets;
    private Queue<Vector3> lookats;
    private Queue<float> times;

    private Vector3 startPosition;
    private Vector3 startLookat;
    private float currentTime;
    private AnimationType animationType;

    void Awake() {
        infinity = transform.forward * 5000f;
        targets = new Queue<Vector3>();
        lookats = new Queue<Vector3>();
        times = new Queue<float>();
        startPosition = transform.position;
        startLookat = infinity;
        currentTime = 0f;
        animationType = AnimationType.Smooth;
    }

    internal void SetAnimationType(AnimationType type) {
        animationType = type;
    }

    void Update() {
        if (targets.Count != 0) {
            Vector3 target = targets.Peek();
            float time = times.Peek();
            Vector3 lookat = lookats.Peek();
            currentTime += Time.deltaTime;
            float t = currentTime / time;
            switch (animationType) {
                case AnimationType.Linear:
                    t = Linear(t);
                    break;
                case AnimationType.Smooth:
                    t = Smooth(t);
                    break;
            }
            if (currentTime > time) {
                currentTime = time;
                t = 1;
            }
            transform.position = Vector3.Lerp(startPosition, target, t);
            if (!lookat.Equals(Vector3.zero)) {
                Vector3 inter = Vector3.Lerp(startLookat, lookat, t);
                transform.LookAt(inter);
            }
            if (currentTime.Equals(time)) {
                times.Dequeue();
                
                startPosition = targets.Dequeue();
                Vector3 last = lookats.Dequeue();
                startLookat = last==Vector3.zero?startLookat:last;
                currentTime = 0f;
            }
        }
    }


    internal void MoveTo(Vector3 pos, float seconds) {
        MoveTo(pos, Vector3.zero, seconds);
    }
    internal void MoveTo(Vector3 pos, Vector3 lookat, float seconds) {
        targets.Enqueue(pos);
        times.Enqueue(seconds);
        lookats.Enqueue(lookat);
    }


    private float Linear(float x) {
        return x;
    }

    private float Smooth(float x) {
        return (float) (1 - Mathf.Cos(x * Mathf.PI)) / 2;
    }

    internal bool IsMoving() {
        return targets.Count > 0;
    }
}
