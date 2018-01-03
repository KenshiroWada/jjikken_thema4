using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation5 : MonoBehaviour {
    private Animator animator;
    public GameObject GameMaster;
    public PreGameMaster GameMaster_script;
    private int i = 0;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        GameMaster = GameObject.Find("PreGameMaster");
        GameMaster_script = GameMaster.GetComponent<PreGameMaster>();
        animator.SetBool("Dead", false);
        i = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (animator.GetBool("Dead"))
        {
            if (i <= 20)
            {
                transform.position += new Vector3(0, -0.05f, 0);
                i++;
            }
        }
	}


    private void OnCollisionEnter(Collision collision)
    {
        //GameObject.Find("player").GetComponent<Animator>().SetBool("Bump", true)
        if (collision.gameObject.Equals(GameObject.Find("player")))
        {
            Debug.Log("GameOver 1");
            GameMaster_script.Gameover();
        }
    }

    public void ReStart()
    {
        transform.position = new Vector3(0, 0.447f, -2);
        animator.SetBool("Dead", false);
        i = 0;
    }

    public void Set()
    {
        animator.SetBool("Dead", false);
    }
}
