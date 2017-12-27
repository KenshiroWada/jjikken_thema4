using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation4 : MonoBehaviour {
    private Animator animator;
    private GameObject player;
    public int judge;
    public int pos = 0;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        animator = player.GetComponent<Animator>();
        animator.SetBool("Bump", false);
    }
	
	// Update is called once per frame
	void Update () {
        judge = Random.Range(0, 1);
        pos++;
        animator.SetBool("Walk", true);
        Vector3 tmp = player.transform.position;
        tmp.z -= 2; 
        if (!animator.GetBool("Bump"))
        {
            transform.position += transform.forward * 0.05f;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        animator.SetBool("Bump", true);
    }
}
