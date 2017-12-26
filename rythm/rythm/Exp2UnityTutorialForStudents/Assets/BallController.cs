using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BallController : MonoBehaviour {
    float Speed = 20.0f;

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(
            (transform.forward + transform.right) * Speed,
            ForceMode.VelocityChange);
	}
	// Update is called once per frame
	void Update () { }
}
