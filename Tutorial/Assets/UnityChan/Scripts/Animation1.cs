using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation1 : MonoBehaviour {
    private Animator animator;
    private int i = 0;
    public GameObject Cube;
    public GameObject UnityChan;
    public GameObject GameMaster;
    public PreGameMaster GameMaster_script;
	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        animator.SetBool("Bump", false);
        animator.SetBool("Avoid", false);
        GameMaster = GameObject.Find("PreGameMaster");
        GameMaster_script = GameMaster.GetComponent<PreGameMaster>();
    }
	
	// Update is called once per frame
	void Update () {
        if (animator.GetBool("Avoid"))
        {
            if (i == 0)
            {
                transform.Rotate(0, 90, 0);
            }
            if (i <= 30)
            {
                transform.position += transform.forward * 0.05f;
            }
            i++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.Equals(GameObject.Find("player")))
        {
            animator.SetBool("Bump", true);
            GameObject.Find("player").GetComponent<Animator>().SetBool("Bump", true);
            Debug.Log("GameOver 0");
            GameMaster_script.Gameover();
        }
    }

    public void Set()
    {
        animator.SetBool("Bump", false);
        animator.SetBool("Avoid", false);
    }

    public void ReStart()
    {
        i = 0;
        transform.Rotate(0, -90, 0);
        transform.position = new Vector3(0, 0, 0);
        animator.SetBool("Bump", false);
        animator.SetBool("Avoid", false);
    }
}
