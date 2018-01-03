using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opening_UnityChan : MonoBehaviour {
    public Animator animator;
    private float _TimeStep = 6.0f;
    private float _TimeCount;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        _TimeCount = Time.time;
        animator.SetBool("Stop", false);
	}
	
	// Update is called once per frame
	void Update () {
        if (_TimeCount + _TimeStep < Time.time)
        {
            Debug.Log("UnityChan Stop");
            rb.velocity = Vector3.zero;
            animator.SetBool("Stop", true);
        }
        else
        {
            //transform.position += transform.forward * 0.01f;
            //animator.SetBool("Stop", false);
        }
	}

    public void ReStart()
    {
        Debug.Log("UnityChan Reset");
        transform.position = new Vector3(0, 0, -7);
        animator.SetBool("Stop", false);
        _TimeCount = Time.time;
    }
}
