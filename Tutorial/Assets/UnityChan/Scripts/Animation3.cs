using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Animation3 : MonoBehaviour {
    private Animator player_animator;
    private Animator bone_animator;
    private Animator unitychan_animator;
    public GameObject Sword;
    public GameObject Player;
    public GameObject Bone;
    public GameObject UnityChan;
    private int i = 0;

    private GameObject TmpEnemy;
    private Animator Enemy_animator;
    private int tmp = 0;
    private float posz;
    public bool move;
    public float speed;
    // Use this for initialization
    void Start () {
        /*Bone = GameObject.Find("skeleton_animated");
        bone_animator = Bone.GetComponent<Animator>();*/
        UnityChan = GameObject.Find("unitychan");
        unitychan_animator = UnityChan.GetComponent<Animator>();
        Player = GameObject.Find("player");
        player_animator = Player.GetComponent<Animator>();
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
        catch (Exception)
        {

        }
        if (Input.GetKey("left"))
        {   
            if (Sword.activeSelf) {
                try
                {
                    Enemy_animator.SetBool("Dead", true);
                }
                catch (Exception)
                {

                }
            }
            if (i == 0)
            {
                Sword.transform.Rotate(0, 0, -20);
                i++;
            }
            else
            {
                Sword.transform.Rotate(0, 0, -20);
                i--;
            }
        } else if (Input.GetKey("down"))
        {
            Sword.SetActive(true);
        } else if (Input.GetKey("up"))
        {
            try
            {
                Enemy_animator.SetBool("Dead", false);
            }
            catch (Exception)
            {

            }
            Sword.SetActive(false);
        }
        if (!player_animator.GetBool("Bump") && move)
        {
            Sword.transform.position += new Vector3(0, 0, speed);
        }

        try
        {
            if ((TmpEnemy.transform.position.z + 0.3) >= posz)
            {
                tmp++;
                Player.GetComponent<Animation4>().score = tmp;
            }
        }
        catch (Exception e)
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
        move = false;
        Sword.transform.position = new Vector3(-0.1f, 1.3f, 3);
        tmp = 0;
    }

    public void Speed(float s)
    {
        this.speed = -s;
    }
}
