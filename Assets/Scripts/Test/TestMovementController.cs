using UnityEngine;
using System.Collections;

public class TestMovementController : MonoBehaviour {

    public float duration = 5;
    public bool right;
    MovementController moves;

    void Start () {
        moves = GetComponent<MovementController>();
        moves.MoveTo(transform.position + Vector3.up * 5, 2);
        moves.Rotate(Quaternion.Euler(0, 180*(right?-1:1), 0), duration);
    }
    
}
