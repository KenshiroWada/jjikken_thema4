using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation6 : MonoBehaviour {

    private Animator animator;
    private GameObject player;
    public int judge;
    public int pos = 0;
    public bool move;
    // Use this for initialization
    void Start()
    {
        move = false;
    }

    // Update is called once per frame
    void Update()
    {
        judge = Random.Range(0, 1);
        pos++;
        Vector3 tmp = player.transform.position;
        tmp.z -= 2;
        if (move)
        {
            transform.position += transform.forward * 0.05f;
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
}
