using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MovementController : MonoBehaviour {


    private Queue<Movement> moves;

    private Movement last;
    private float currentTime;

    void Awake() {
        moves = new Queue<Movement>();
        last = new Movement(transform.position, transform.rotation, 0f);
        currentTime = 0f;
    }

    void Update() {
        if (moves.Count != 0) {
            Movement move = moves.Peek();
            currentTime += Time.deltaTime;
            float t = currentTime / move.time;
            t = move.ApplyTransform(t);

            if (currentTime > move.time) {
                currentTime = move.time;
                t = 1;
            }
            if (!move.position.Equals(Vector3.zero)) {
                transform.position = Vector3.Lerp(last.position, move.position, t);
            }
            if (!move.rotation.Equals(Quaternion.identity)) {
                transform.rotation = Quaternion.Lerp(last.rotation, move.rotation, t);
            }
            if (currentTime.Equals(move.time)) {
                moves.Dequeue();
                last.position = move.position;
                last.rotation = move.rotation;
                currentTime = 0f;
            }
        }
    }

    internal void Rotate(Quaternion rot, float seconds) {
        MoveTo(Vector3.zero, rot, seconds);
    }

    internal void MoveTo(Vector3 pos, float seconds) {
        MoveTo(pos, Quaternion.identity, seconds);
    }
    internal void MoveTo(Vector3 pos, Quaternion rot, float seconds) {
        moves.Enqueue(new Movement(pos, rot, seconds));
    }

    internal bool IsMoving() {
        return moves.Count > 0;
    }
}
