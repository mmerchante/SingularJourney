using UnityEngine;
using System.Collections;

public class MoveZ : MonoBehaviour {

	void Update () 
    {
        this.transform.position += Vector3.forward * Time.deltaTime;
	}
}
