using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animation2 : MonoBehaviour {
    private int i = 0;
    public GameObject Light;
    public GameObject Player;
    private Animator Player_animator;
    private GameObject TmpEnemy;
    private Animator Enemy_animator;
    private int tmp = 0;
    private float posz;
    public bool move;
    public float speed;
    // Use this for initialization
    void Start () {
        Light.SetActive(false);
        Player = GameObject.Find("player");
        Player_animator = Player.GetComponent<Animator>();
        move = false;
    }
	
	// Update is called once per frame
	void Update () { 
        posz = Player.transform.position.z;
        try
        {
            TmpEnemy = GameObject.Find("Enemy" + tmp);
            Enemy_animator = TmpEnemy.GetComponent<Animator>();
        }
        catch(Exception)
        {
            //TmpEnemy = GameObject.Find("Enemy0");
            //Enemy_animator = TmpEnemy.GetComponent<Animator>();
        }

        if (Input.GetKey("left")) {
            if (Light.activeSelf)
            {
                try
                {
                    Enemy_animator.SetBool("Avoid", true);
                }
                catch(Exception)
                {

                }
            }
            if (i < 6) {
                Light.transform.Rotate(0, 0, -20);
                i++;
            } else if (i < 12) {
                Light.transform.Rotate(0, 0, 20);
                i++;
            } else {
                i = 0;
            }
        }
        if (Input.GetKey("down"))
        {
            try
            {
                Enemy_animator.SetBool("Avoid", false);
            }
            catch (Exception)
            {

            }
            Light.SetActive(false);
        }
        if (Input.GetKey("up"))
        {
            Light.SetActive(true);
        }
        try
        {
            if (!Player_animator.GetBool("Bump") && move)
            {
                Light.transform.position += new Vector3(0, 0, speed);
            }
        }
        catch(Exception)
        {

        }
        try
        {
            if ((TmpEnemy.transform.position.z + 0.3) >= posz)
            {
                tmp++;
                Player.GetComponent<Animation4>().score = tmp;
            }
        }
        catch(Exception e)
        {

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

    public void ReStart()
    {
        Light.transform.position = new Vector3(0, 1.2f, 3);
        Light.SetActive(false);
        this.move = false;
        tmp = 0;
    }

    public void Speed(float s)
    {
        this.speed = -s;
    }
}
