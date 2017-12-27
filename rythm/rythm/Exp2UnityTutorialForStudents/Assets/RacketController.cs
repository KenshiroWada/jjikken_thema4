using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RacketController : MonoBehaviour {
    float Accel = 1000.0f;


	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody>().AddForce(
            transform.right * Input.GetAxisRaw("Horizontal") * Accel,
            ForceMode.Impulse);
	}
}
