using UnityEngine;
using System.Collections;

public class EndgameCollider : MonoBehaviour {

    void OnTriggerEnter()
    {
        Application.LoadLevel("Credits");
    }
}
