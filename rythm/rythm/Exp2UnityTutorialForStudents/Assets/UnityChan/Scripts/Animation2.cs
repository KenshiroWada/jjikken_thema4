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
	private float before_posx = 0;
    // Use this for initialization
    void Start () {
        //Light.SetActive(false);
        Player = GameObject.Find("player");
        Player_animator = Player.GetComponent<Animator>();
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
            TmpEnemy = GameObject.Find("Enemy0");
            Enemy_animator = TmpEnemy.GetComponent<Animator>();
        }

        //if (Input.GetKey("left")) {
		if ((float)PSMoveDetect.Sx - before_posx > 3 || (float)PSMoveDetect.Sx - before_posx < -3) {
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
            if (!Player_animator.GetBool("Bump"))
            {
                Light.transform.position += new Vector3(0, 0, -0.05f);
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
            }
        }
        catch(Exception)
        {

        }
		before_posx = (float)PSMoveDetect.Sx;

    }
}
