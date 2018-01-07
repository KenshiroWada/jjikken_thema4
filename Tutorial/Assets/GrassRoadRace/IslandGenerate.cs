using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IslandGenerate : MonoBehaviour
{
    public GameObject IslandPrefab;
    private GameObject _IslandInstance;

    private int i = 0;
    private int xPos = 3;
    private int zPos = 0;

    // Use this for initialization
    void Start()
    {
        for (i = 0; i < 20; i++)
        {
            GenerateIsland();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateIsland()
    {

        _IslandInstance = Instantiate(IslandPrefab, new Vector3(xPos, 0, zPos), Quaternion.identity);
        _IslandInstance.name = "Island" + i;
        _IslandInstance.transform.SetParent(transform, false);
        if (xPos == 3)
        {
            xPos = -3;
        }
        else if (xPos == -3)
        {
            xPos = 3;
        }
        zPos -= 5;

    }
}
