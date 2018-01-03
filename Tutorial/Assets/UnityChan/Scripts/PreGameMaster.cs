using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class PreGameMaster : MonoBehaviour {
    public GameObject Sword, Light, Player, EnemyMaster, Unitychan, Cube, Enemy0, Enemy1, Camera, LightManager, SwordManager;
    private float _TimeCount;
    private float _Opening_Time = 7.0f, _Countdown_Time1 = 1.0f, _Countdown_Time2 = 2.0f, _Countdown_Time3 = 3.0f, _Gameover_Time = 5.0f;
    private Animator animator;
    private Rigidbody rb;
    private bool opening, countdown, playing, gameover, clear;
    public Text text;
    private Animation1 Enemy0_scripts;
    private Animation2 Light_scripts;
    private Animation3 Sword_scripts;
    private Animation4 Player_scripts;
    private Animation5 Enemy1_scripts;
    private Animation4 Camera_scripts;
    private Opening_UnityChan UnityChan_scripts;
    private EnemyGenerator EnemyMaster_scripts;
    private float HIGH = 0.08f, NORMAL = 0.05f;
    // Use this for initialization
    void Start () {
        try
        {
            text = GameObject.Find("Text").GetComponent<Text>();
            animator = Unitychan.GetComponent<Animator>();
            rb = Unitychan.GetComponent<Rigidbody>();
            Enemy0_scripts = Enemy0.GetComponent<Animation1>();
            Enemy1_scripts = Enemy1.GetComponent<Animation5>();
            Light_scripts = LightManager.GetComponent<Animation2>();
            Sword_scripts = SwordManager.GetComponent<Animation3>();
            Player_scripts = Player.GetComponent<Animation4>();
            Camera_scripts = Camera.GetComponent<Animation4>();
            UnityChan_scripts = Unitychan.GetComponent<Opening_UnityChan>();
            EnemyMaster_scripts = EnemyMaster.GetComponent<EnemyGenerator>();
            opening = true;
            countdown = false;
            playing = false;
            gameover = false;
            clear = false;
        }
        catch (Exception e)
        {
            Debug.Log("exception");
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (opening)
        {
            Player_scripts.score = 0;
            text.text = "SELECT LEVEL ○easy ×normal PRESS ANY BUTTON";
            Light.SetActive(false);
            Sword.SetActive(false);
            Player.SetActive(false);
            EnemyMaster.SetActive(false);
            Enemy0.SetActive(false);
            Enemy1.SetActive(false);
            Unitychan.SetActive(true);
            Cube.SetActive(false);

            if (_TimeCount + _Opening_Time < Time.time)
            {
                //rb.velocity = Vector3.zero;
                //animator.SetBool("Stop", true);
                Cube.SetActive(true);
                // ボタンイベント　UnityのInput.GetButtonと同じ仕組み
                if (Input.GetKey("up"))
                {
                    _TimeCount = Time.time;
                    opening = false;
                    countdown = true;
                    NormalSpeed();
                }
                Debug.Log("Success");
                if (Input.GetKey("down"))
                {
                    _TimeCount = Time.time;
                    opening = false;
                    countdown = true;
                    HighSpeed();
                }
            }
            else
            {
                Unitychan.transform.position += transform.forward * 0.035f;
                animator.SetBool("Stop", false);
            }
        }
        if (countdown)
        {
            Light.SetActive(false);
            Sword.SetActive(false);
            Player.SetActive(false);
            EnemyMaster.SetActive(false);
            Enemy0.SetActive(false);
            Enemy1.SetActive(false);
            Unitychan.SetActive(false);
            Cube.SetActive(true);
            if (_TimeCount + _Countdown_Time1 > Time.time)
            {
                text.text = "3";
            }
            else if (_TimeCount + _Countdown_Time2 > Time.time)
            {
                text.text = "2";
            }
            else if (_TimeCount + _Countdown_Time3 > Time.time)
            {
                text.text = "1";
            }
            else
            {
                countdown = false;
                playing = true;
                _TimeCount = Time.time;
                Light.SetActive(false);
                Sword.SetActive(true);
                Unitychan.SetActive(false);
                Cube.SetActive(false);
                Player.SetActive(true);
                Player_scripts.Set();
                Enemy0.SetActive(true);
                Enemy0_scripts.Set();
                Enemy1.SetActive(true);
                Enemy1_scripts.Set();
                EnemyMaster.SetActive(true);
                Player_scripts.Move();
                Camera_scripts.Move();
                Light_scripts.Move();
                Sword_scripts.Move();
            }
        }


        if (playing)
        {
            if (Input.GetKey("up"))
            {
                Debug.Log("Circle Down");
                Light.SetActive(true);
                Sword.SetActive(false);
            }
            Debug.Log("Success");
            if (Input.GetKey("down"))
            {
                Debug.Log("Circle UP");
                Light.SetActive(false);
                Sword.SetActive(true);
            }
        }

        if (gameover)
        {

            if (_TimeCount + _Gameover_Time < Time.time)
            {
                ReStart();
                this.gameover = true;
                Light.SetActive(false);
                Sword.SetActive(false);
                Player.SetActive(false);
                EnemyMaster.SetActive(true);
                Enemy0.SetActive(false);
                Enemy1.SetActive(false);
                Unitychan.SetActive(false);
                Cube.SetActive(true);
                text.text = "Score " + Player_scripts.score + "press ○ to ReStart";

                if (Input.GetKey("up"))
                {
                    
                    Opening();
                    _TimeCount = Time.time;
                    UnityChan_scripts.ReStart();
                    Light.SetActive(false);
                    Sword.SetActive(false);
                    Player.SetActive(false);
                    EnemyMaster.SetActive(false);
                    Enemy0.SetActive(false);
                    Enemy1.SetActive(false);
                    Unitychan.SetActive(true);
                    Cube.SetActive(false);
                    Enemy0_scripts.ReStart();
                }
            }
        }
    }



    public void Opening()
    {
        this.opening = true;
        this.countdown = false;
        this.playing = false;
        this.gameover = false;
        this.clear = false;
        _TimeCount = Time.time;
    }

    public void Countdown()
    {
        this.opening = false;
        this.countdown = true;
        this.playing = false;
        this.gameover = false;
        this.clear = false;
    }

    public void Playing()
    {
        this.opening = false;
        this.countdown = false;
        this.playing = true;
        this.gameover = false;
        this.clear = false;
    }

    public void Gameover()
    {
        this.opening = false;
        this.countdown = false;
        this.playing = false;
        this.gameover = true;
        this.clear = false;
        _TimeCount = Time.time;
    }

    public void Clear()
    {
        this.opening = false;
        this.countdown = false;
        this.playing = false;
        this.gameover = false;
        this.clear = true;
    }

    public void ReStart()
    {
        EnemyMaster_scripts.ReStart();
        //Enemy0_scripts.ReStart();
        Light_scripts.ReStart();
        Sword_scripts.ReStart();
        Player_scripts.ReStart();
        Enemy1_scripts.ReStart();
        Camera_scripts.CameraReStart();
        UnityChan_scripts.ReStart();
        opening = false;
        countdown = false;
        playing = false;
        gameover = false;
        clear = false;
        animator.SetBool("Stop", false);
    }

    public void HighSpeed()
    {
        Light_scripts.Speed(HIGH);
        Sword_scripts.Speed(HIGH);
        Player_scripts.Speed(HIGH);
        Camera_scripts.Speed(HIGH);
    }

    public void NormalSpeed()
    {
        Light_scripts.Speed(NORMAL);
        Sword_scripts.Speed(NORMAL);
        Player_scripts.Speed(NORMAL);
        Camera_scripts.Speed(NORMAL);
    }
}
