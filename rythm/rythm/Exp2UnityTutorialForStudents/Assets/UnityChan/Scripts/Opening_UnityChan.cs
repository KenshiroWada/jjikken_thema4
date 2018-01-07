using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Opening_UnityChan : MonoBehaviour {
    // PSMOVEの初期セッティング
    const float Collide_time = 0.15f;
    // We save a list of Move controllers.
    List<UniMoveController> _Moves = new List<UniMoveController>();
    GUIStyle _Style;
    public GUIStyleState StyleState;

    public Animator animator;
    public GameObject UnityChan;
    private float _TimeStep = 7.0f;
    private float _TimeCount;
    private GameObject Cube;
    public Rigidbody rb;
	// Use this for initialization
	void Start () {
        UnityChan = GameObject.Find("unitychan");
        animator = UnityChan.GetComponent<Animator>();
        rb = UnityChan.GetComponent<Rigidbody>();
        _TimeCount = Time.time;
        animator.SetBool("Stop", false);
        Cube = GameObject.Find("Cube");
        Cube.SetActive(false);

        _Style = new GUIStyle();
        _Style.fontSize = 20;

        Time.maximumDeltaTime = 0.1f;

        int count = UniMoveController.GetNumConnected();

        // Iterate through all connections (USB and Bluetooth)
        for (int i = 0; i < count; i++)
        {
            UniMoveController move = gameObject.AddComponent<UniMoveController>();  // It's a MonoBehaviour, so we can't just call a constructor

            // Remember to initialize!
            if (move.Init(i) == PSMove_Connect_Status.MoveConnect_NoData)
            {
                Destroy(move);  // If it failed to initialize, destroy and continue on
                continue;
            }

            // This example program only uses Bluetooth-connected controllers
            PSMoveConnectionType conn = move.ConnectionType;
            if (conn == PSMoveConnectionType.Unknown || conn == PSMoveConnectionType.USB)
            {
                Destroy(move);
            }
            else
            {
                _Moves.Add(move);
                move.OnControllerDisconnected += HandleControllerDisconnected;

                // 球体LEDの基本セッティング　赤色
                move.SetLED(Color.red);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        foreach (UniMoveController move in _Moves) //PSMoveが正常的に繋がっている時に実行される
        {
            // Instead of this somewhat kludge-y check, we'd probably want to remove/destroy
            // the now-defunct controller in the disconnected event handler below.
            if (move.Disconnected) continue;

            // 押すボタンによって球体のLEDの色が変わる
            if (move.GetButtonDown(PSMoveButton.Circle)) move.SetLED(Color.red);
            else if (move.GetButtonDown(PSMoveButton.Cross)) move.SetLED(Color.red);
            else if (move.GetButtonDown(PSMoveButton.Square)) move.SetLED(Color.red);
            else if (move.GetButtonDown(PSMoveButton.Triangle)) move.SetLED(Color.red);
            else if (move.GetButtonDown(PSMoveButton.Move))
            {
                move.SetLED(Color.black);
            }

            // トリガーボタンが押されたら振動する
            move.SetRumble(move.Trigger);



            if (_TimeCount + _TimeStep < Time.time)
            {
                rb.velocity = Vector3.zero;
                animator.SetBool("Stop", true);
                Cube.SetActive(true);
                // ボタンイベント　UnityのInput.GetButtonと同じ仕組み
                if (move.GetButtonDown(PSMoveButton.Circle))
                {

                }
                Debug.Log("Success");
                if (move.GetButtonUp(PSMoveButton.Cross))
                {

                }
            }
            else
            {
                UnityChan.transform.position += UnityChan.transform.forward * 0.01f;
            }
        }
	}

    void HandleControllerDisconnected(object sender, EventArgs e) //
    {
        // TODO: Remove this disconnected controller from the list and maybe give an update to the player
    }

    void OnGUI()
    {
        string display = "";

        if (_Moves.Count > 0)
        {
            for (int i = 0; i < _Moves.Count; i++)
            {
                display += string.Format("PS Move {0}: ax:{1:0.000}, ay:{2:0.000}, az:{3:0.000} gx:{4:0.000}, gy:{5:0.000}, gz:{6:0.000}, posx:{7:0.000}, posy:{8:0.000}, posz:{9:0.000}\n",
                                         i + 1, _Moves[i].Acceleration.x, _Moves[i].Acceleration.y, _Moves[i].Acceleration.z,
                                         _Moves[i].Gyro.x, _Moves[i].Gyro.y, _Moves[i].Gyro.z, PSMoveDetect.Sx, PSMoveDetect.Sy, PSMoveDetect.Sz);
                //PSmoveの値などを画面にテキストで表示 ax,ay,az:加速度センサーのx,y,z値 gx,gy,gz:ジャイロセンサーのx,y,z値 posx,posy,posz:球体のx,y,z値
            }
        }
        else display = "No Bluetooth-connected controllers found. Make sure one or more are both paired and connected to this computer.";
        GUI.Label(new Rect(10, Screen.height - 100, 500, 100), display);

        _Style.normal = StyleState;
        StyleState.textColor = Color.white;

        /*if (Left_touch){
			GUI.Label( new Rect(Screen.width/4, Screen.height/4, Screen.width/4, Screen.height/4), "   Racket is touching Left wall!" ,_Style);
		}
		if (Right_touch) {
			GUI.Label( new Rect(Screen.width/4, Screen.height/4, Screen.width/4, Screen.height/4), "  Racket is touching Right wall!" ,_Style);
		}*/
    }
}
