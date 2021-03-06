/**
 * UniMove API - A Unity plugin for the PlayStation Move motion controller
 * CopyRight (C) 2012, 2013, Copenhagen Game Collective (http://www.cphgc.org)
 * 					         Patrick Jarnfelt
 * 					         Douglas Wilson (http://www.doougle.net)
 * 
 * 
 * All Rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 *    1. Redistributions of source code must retain the above copyRight
 *       notice, this list of conditions and the following disclaimer.
 *
 *    2. Redistributions in binary form must reproduce the above copyRight
 *       notice, this list of conditions and the following disclaimer in the
 *       documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 **/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;


public class UniMoveTest : MonoBehaviour
{
    // PSMOVEの初期セッティング
	const float Collide_time = 0.15f;
    // We save a list of Move controllers.
    List<UniMoveController> _Moves = new List<UniMoveController>();
    GUIStyle _Style;
	public GUIStyleState StyleState;
	public GameObject Left, Right, Racket, Sword, Light, Player, EnemyMaster, Unitychan, Cube, Enemy0, Enemy1, Camera;
	public bool Left_touch = false;
	public bool Right_touch = false;
	public bool IsCollided = false;
	public float Timer = 0;
	public float before_posx = 0;
	public int i = 0;
    private float _TimeCount;
    private float _Opening_Time = 7.0f, _Countdown_Time1 = 1.0f, _Countdown_Time2 = 2.0f, _Countdown_Time3 = 3.0f;
    private Animator animator;
    private Rigidbody rb;
    private bool opening, countdown, playing, gameover, clear;
    private Text text;

    void Start() 
	{
		/* NOTE! We recommend that you limit the maximum frequency between frames.
		 * This is because the controllers use Update() and not FixedUpdate(),
		 * and yet need to update often enough to respond sufficiently fast.
		 * Unity advises to keep this value "between 1/10th and 1/3th of a second."
		 * However, even 100 milliseconds could seem slightly sluggish, so you
		 * might want to experiment w/ reducing this value even more.
		 * Obviously, this should only be relevant in case your framerare is starting
		 * to lag. Most of the time, Update() should be called very regularly.
		 */

		// If you have changed these names, please replace below "names"
		/*Left = GameObject.Find("LeftWall");
		Right = GameObject.Find("RightWall");
		Racket = GameObject.Find("Racket");*/
		try {
			Sword = GameObject.Find ("Sword");
			Light = GameObject.Find ("VolumetricLinePrefab");
            Player = GameObject.Find("player");
            EnemyMaster = GameObject.Find("EnemyMaster");
            Unitychan = GameObject.Find("unitychan");
            Cube = GameObject.Find("Cube");
            Enemy0 = GameObject.Find("Enemy0");
            Enemy1 = GameObject.Find("Enemy1");
            Camera = GameObject.Find("Main Camera");
            text = GameObject.Find("Text").GetComponent<Text>();
            animator = Unitychan.GetComponent<Animator>();
            rb = Unitychan.GetComponent<Rigidbody>();
            opening = true;
            countdown = false;
            playing = false;
            gameover = false;
            clear = false;
        } catch (Exception e) {
			Debug.Log ("exception");
		} 

		Light.SetActive (false);
		Sword.SetActive (false);
        Player.SetActive(false);
        EnemyMaster.SetActive(false);
        Enemy0.SetActive(false);
        Enemy1.SetActive(false);
        Unitychan.SetActive(false);
        Cube.SetActive(false);


		_Style = new GUIStyle();
		_Style.fontSize = 20;

		Time.maximumDeltaTime = 0.1f;
		
		int count = UniMoveController.GetNumConnected();
		
		// Iterate through all connections (USB and Bluetooth)
		for (int i = 0; i < count; i++) 
		{
			UniMoveController move = gameObject.AddComponent<UniMoveController>();	// It's a MonoBehaviour, so we can't just call a constructor
			
			// Remember to initialize!
			if (move.Init(i) == PSMove_Connect_Status.MoveConnect_NoData) 
			{	
				Destroy(move);	// If it failed to initialize, destroy and continue on
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
        _TimeCount = Time.time;
    }

    // このオブジェクトが衝突する時実行される関数
    void OnCollisionEnter(Collision col)
	{
        if (col.gameObject.name == "Ball")// 衝突したオブジェクトの名前がBallだったら
		{
            IsCollided = true;	// このオブジェクトが衝突すると変数IsCollidedの状態をtrueに変える
		}
		else
		{
            IsCollided = false;	// 衝突しないときはfalseに
		}
	}

    void Update() //1フレームが更新されるたび呼び出される関数
	{

        foreach (UniMoveController move in _Moves) //PSMoveが正常的に繋がっている時に実行される
		{
			// Instead of this somewhat kludge-y check, we'd probably want to remove/destroy
			// the now-defunct controller in the disconnected event handler below.
			if (move.Disconnected) continue;
			
			// ボタンイベント　UnityのInput.GetButtonと同じ仕組み
			if (move.GetButtonDown(PSMoveButton.Circle) && playing){
				Debug.Log ("Circle Down");
				Light.SetActive (true);
				Debug.Log("Circle 1");
				Sword.SetActive (false);
				Debug.Log("Circle 2");
			}
			Debug.Log ("Success");
			if (move.GetButtonUp(PSMoveButton.Cross) && playing){
				Debug.Log("Circle UP");
				Light.SetActive (false);
				Sword.SetActive (true);
			}
			
			// 押すボタンによって球体のLEDの色が変わる
			if (move.GetButtonDown(PSMoveButton.Circle)) 		move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Cross)) 	move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Square)) 	move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Triangle)) 	move.SetLED(Color.red);
			else if(move.GetButtonDown(PSMoveButton.Move)) {
				move.SetLED(Color.black);
			}

            // トリガーボタンが押されたら振動する
            move.SetRumble(move.Trigger);

			// transform.localPosition = move.Position;
			transform.localRotation = move.Orientation;
			
			// PSMoveの球体の座標によってオブジェクトの座標が変わる
			/*if ((float)PSMoveDetect.Sx > Left.transform.position.x + Racket.transform.localScale.x/2 
			    && (float)PSMoveDetect.Sx < Right.transform.position.x - Racket.transform.localScale.x/2) //ゲーム内ではみ出さないようにオブジェクトの移動範囲を制限
            {
				transform.position = new Vector3(((float)PSMoveDetect.Sx), GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
				Left_touch = false;
				Right_touch = false;
			}
			else if ((float)PSMoveDetect.Sx <= Left.transform.position.x)
			{
				transform.position = new Vector3(Left.transform.position.x + Racket.transform.localScale.x/2, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
				Left_touch = true; 
			}

			else if ((float)PSMoveDetect.Sx >= Right.transform.position.x)
			{
				transform.position = new Vector3(Right.transform.position.x - Racket.transform.localScale.x/2, GetComponent<Rigidbody>().position.y, GetComponent<Rigidbody>().position.z);
				Right_touch = true;
			}*/


			//PSMoveの球体の座標によってオブジェクトに掛かる力量が変わる
			//rigidbody.AddForce( 
			//                 transform.Right * Input.GetAxisRaw( "Horizontal" ) * Accel, 
			//               ForceMode.Impulse );

            //if( (float)PSMoveDetect.Sx / 3 > 0)
            //	rigidbody.AddForce(transform.Right * ((float)PSMoveDetect.Sx), ForceMode.Impulse );

            
            if (IsCollided == true) //関数OnCollisionEnterでIsCollidedの値がtrueに変わった場合
            {
                Timer += Time.deltaTime; //0から時間を計る
                if (Timer < Collide_time) //計った時間が0からCollide_time以内の場合
				{
					move.SetLED(Color.magenta); //LEDの色を赤色にする
                    move.SetRumble(1f); //1の硬度で振動する
				}
                else //計った時間が15msを超える場合
				{
                    Timer = 0; //タイマーを0に戻す
					move.SetLED(Color.red);
					IsCollided = false;
				}
			}

            if (opening)
            {
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
                    rb.velocity = Vector3.zero;
                    animator.SetBool("Stop", true);
                    Cube.SetActive(true);
                    // ボタンイベント　UnityのInput.GetButtonと同じ仕組み
                    if (move.GetButtonDown(PSMoveButton.Circle))
                    {
                        _TimeCount = Time.time;
                        opening = false;
                        countdown = true;
                    }
                    Debug.Log("Success");
                    if (move.GetButtonUp(PSMoveButton.Cross))
                    {
                        _TimeCount = Time.time;
                        opening = false;
                        countdown = true;
                    }
                }
                else
                {
                    Unitychan.transform.position += Unitychan.transform.forward * 0.01f;
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
                if (_TimeCount + _Countdown_Time1 < Time.time)
                {
                    text.text = "1";
                } else if (_TimeCount + _Countdown_Time2 < Time.time)
                {
                    text.text = "2";
                } else if (_TimeCount + _Countdown_Time3 < Time.time)
                {
                    text.text = "3";
                } else
                {
                    countdown = false;
                    playing = true;
                    _TimeCount = Time.time;
                }
            }
        }

    }
	
	
	void HandleControllerDisconnected (object sender, EventArgs e) //
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
				                         i+1, _Moves[i].Acceleration.x, _Moves[i].Acceleration.y, _Moves[i].Acceleration.z,
                                         _Moves[i].Gyro.x, _Moves[i].Gyro.y, _Moves[i].Gyro.z, PSMoveDetect.Sx, PSMoveDetect.Sy, PSMoveDetect.Sz);
                                //PSmoveの値などを画面にテキストで表示 ax,ay,az:加速度センサーのx,y,z値 gx,gy,gz:ジャイロセンサーのx,y,z値 posx,posy,posz:球体のx,y,z値
			}
		}
		else display = "No Bluetooth-connected controllers found. Make sure one or more are both paired and connected to this computer.";
		GUI.Label(new Rect(10, Screen.height-100, 500, 100), display);

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
