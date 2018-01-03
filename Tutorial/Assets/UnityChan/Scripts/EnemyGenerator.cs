using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour {
    public GameObject BonePrefab;
    private GameObject _BoneInstance;
    public GameObject UnityChanPrefab;
    private GameObject _UnityChanInstance;
    private GameObject _EnemyInstance;

    private static int _EnemyMax = 30;
    private static int _EnemyCount = 2;

    private int _EnemyStepZ = -2;

    private int _EnemyInitZ = 0;
    private float _BoneInitY = 0.447f;
    private int _UnityChanInitY = 0;

    private float _TimeStep = 0.1f;
    private float _TimeCount;

    private int i = 29;

	// Use this for initialization
	void Start () {
        _TimeCount = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (_TimeCount + _TimeStep < Time.time)
        {
            GenerateEnemy();
            _TimeCount = Time.time;
        }
	}

    void GenerateEnemy()
    {
        if (_EnemyCount < _EnemyMax)
        {
            int zPos = _EnemyCount * _EnemyStepZ + _EnemyInitZ;
            if (Random.Range(0, 2) == 0)
            {
                float yPos = _BoneInitY;
                _EnemyInstance = Instantiate(BonePrefab, new Vector3(0, yPos, zPos), Quaternion.identity);
                _EnemyInstance.name = "Enemy" + _EnemyCount;
                _EnemyInstance.transform.SetParent(transform, false);
            } else
            {
                float yPos = _UnityChanInitY;
                _EnemyInstance = Instantiate(UnityChanPrefab, new Vector3(0, yPos, zPos), Quaternion.identity);
                _EnemyInstance.name = "Enemy" + _EnemyCount;
                _EnemyInstance.transform.SetParent(transform, false);
            }
            _EnemyCount++;
        }
    }

    public void ReStart()
    {
        _TimeCount = Time.time;
        _EnemyCount = 2;
        while (i > 1)
        {
            GameObject enemy;
            enemy = GameObject.Find("Enemy" + i);
            Destroy(enemy);
            i--;
        }
        i = 29;
    }
}
