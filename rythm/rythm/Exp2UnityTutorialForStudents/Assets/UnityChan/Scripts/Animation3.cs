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
	private float before_posx = 0;

    private GameObject TmpEnemy;
    private Animator Enemy_animator;
    private int tmp = 0;
    private float posz;
    // Use this for initialization
    void Start () {
		Sword = GameObject.Find ("Sword");
        Bone = GameObject.Find("Enemy1");
        bone_animator = Bone.GetComponent<Animator>();
        UnityChan = GameObject.Find("Enemy0");
        unitychan_animator = UnityChan.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Player = GameObject.Find("player");
        player_animator = Player.GetComponent<Animator>();

        posz = Player.transform.position.z;
        try
        {
            TmpEnemy = GameObject.Find("Enemy" + tmp);
            Enemy_animator = TmpEnemy.GetComponent<Animator>();
        }
        catch (Exception)
        {

        }
        //if (Input.GetKey("left"))
		Debug.Log("far" + ((float)PSMoveDetect.Sx - before_posx));
		if ((float)PSMoveDetect.Sx - before_posx > 3 || (float)PSMoveDetect.Sx - before_posx < -3)
        {   
			Debug.Log ("Kiteru");
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
        if (!player_animator.GetBool("Bump"))
        {
            Sword.transform.position += new Vector3(0, 0, -0.05f);
        }

        try
        {
            if ((TmpEnemy.transform.position.z + 0.3) >= posz)
            {
                tmp++;
            }
        }
        catch (Exception e)
        {

        }
		before_posx = (float)PSMoveDetect.Sx;

    }
}
