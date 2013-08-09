using UnityEngine;
using System.Collections;

public class Movement {

    public enum AnimationType {
        Linear,
        Smooth
    }

    public AnimationType animationType;
    public Vector3 position;
    public Quaternion rotation;
    public float time;


    public Movement(Vector3 position, Quaternion rotation, float time) {
        this.position = position;
        this.rotation = rotation;
        this.time = time;
    }

    private float Linear(float x) {
        return x;
    }

    private float Smooth(float x) {
        return (float) (1 - Mathf.Cos(x * Mathf.PI)) / 2;
    }


    internal float ApplyTransform(float t) {
        switch (animationType) {
            case Movement.AnimationType.Linear:
                return Linear(t);
            case Movement.AnimationType.Smooth:
                return Smooth(t);
        }
        return t;
    }
}
