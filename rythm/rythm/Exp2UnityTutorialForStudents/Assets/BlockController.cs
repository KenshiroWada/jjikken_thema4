using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour {
	// Use this for initialization
	void Start () { }
	
	// Update is called once per frame
	void Update () { }

    void OnCollisionEnter(Collision collision)
    {
        // ボールの色をボールが当たったブロックの色に変更
        // ボールが当たったブロックを破棄
        GameObject.Find("Ball").GetComponent<Renderer>().material.color
            = GetComponent<Renderer>().material.color;
        Destroy(gameObject);
    }
}
