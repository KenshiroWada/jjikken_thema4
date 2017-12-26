using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1 : MonoBehaviour {
    private Animator animator;
    private int i = 0;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Bump", false);
        animator.SetBool("Avoid", false);
    }
	
	// Update is called once per frame
	void Update () {
        if (animator.GetBool("Avoid"))
        {
            if (i == 0)
            {
                transform.Rotate(0, 90, 0);
            }
            if (i <= 1000)
            {
                transform.position += transform.forward * 0.05f;
            }
            i++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("Bump", true);
        GameObject.Find("player").GetComponent<Animator>().SetBool("Bump", true);
    }
}
