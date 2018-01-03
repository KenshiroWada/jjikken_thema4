using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation4 : MonoBehaviour {
    private Animator animator;
    private GameObject player;
    public int judge;
    public int pos = 0;
    public bool move;
    public int score = 4;
    public float speed;
    // Use this for initialization
    void Start () {
        player = GameObject.Find("player");
        animator = player.GetComponent<Animator>();
        animator.SetBool("Bump", false);
        move = false;
    }
	
	// Update is called once per frame
	void Update () {
        judge = Random.Range(0, 1);
        pos++;
        animator.SetBool("Walk", true);
        Vector3 tmp = player.transform.position;
        tmp.z -= 2; 
        if (!animator.GetBool("Bump") && move)
        {
            transform.position += transform.forward * speed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            animator.SetBool("Bump", true);
        }

    }

    public void Stop()
    {
        this.move = false;
    }

    public void Move()
    {
        this.move = true;
    }

    public void Set()
    {
        animator.SetBool("Bump", false);
    }

    public void ReStart()
    {
        move = false;
        transform.position = new Vector3(0, 0, 3.3f);
    }

    public void CameraReStart()
    {
        move = false;
        transform.position = new Vector3(0, 2, 5);
    }

    public void Speed(float s)
    {
        this.speed = s;
    }
}
