using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnalyser : MonoBehaviour
{
    public float landTileCount = 0, waterTileCount = 0, forestTileCount = 0;
    GameObject[] waterTileList, landTileList, forestTileList;
    public float waterPollutionAmount = 0, landPollutionAmount = 0, airPollutionAmount = 0;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getTileCount()
    {
        waterTileList = GameObject.FindGameObjectsWithTag("Water");
        landTileList = GameObject.FindGameObjectsWithTag("Land");
        forestTileList = GameObject.FindGameObjectsWithTag("Forest");

        landTileCount = landTileList.Length;
        waterTileCount = waterTileList.Length;
        forestTileCount = forestTileList.Length;
    }

    public void getPollution()
    {
        waterPollutionAmount = 0;
        landPollutionAmount = 0;
        airPollutionAmount = 0;

        for(int i=0;i<waterTileList.Length;i++)
        {
            waterPollutionAmount += waterTileList[i].GetComponent<F_Tile>().waterPollution;
            Debug.Log(waterPollutionAmount);
            airPollutionAmount += waterTileList[i].GetComponent<F_Tile>().airPollution;
        }
        for (int i = 0; i < landTileList.Length; i++)
        {
            landPollutionAmount += landTileList[i].GetComponent<F_Tile>().landPollution;
            airPollutionAmount += landTileList[i].GetComponent<F_Tile>().airPollution;
        }
        for (int i = 0; i < forestTileList.Length; i++)
        {
            landPollutionAmount += forestTileList[i].GetComponent<F_Tile>().landPollution;
            airPollutionAmount += forestTileList[i].GetComponent<F_Tile>().airPollution;
        }
    }
}
