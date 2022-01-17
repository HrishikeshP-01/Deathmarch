using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentAnalyser : MonoBehaviour
{
    public float landTileCount = 0, waterTileCount = 0, forestTileCount = 0;
    GameObject[] waterTileList, landTileList, forestTileList;
    public GameObject ForestPrefab;
    public float waterQualityAmount = 0, landQualityAmount = 0, airQualityAmount = 0, economy = 0, food = 0, houses = 0;

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

    public void getQuality()
    {
        // Max possible quality amount
        waterQualityAmount = 0;
        landQualityAmount = 0;
        airQualityAmount = 0;

        /*
        for (int i=0;i<waterTileList.Length;i++)
        {
            waterQualityAmount += waterTileList[i].GetComponent<F_Tile>().waterQuality;
            airQualityAmount += waterTileList[i].GetComponent<F_Tile>().airQuality;
        }
        */
        
        for (int i = 0; i < landTileList.Length; i++)
        {
            landQualityAmount += landTileList[i].GetComponent<F_Tile>().landQuality;
            airQualityAmount += landTileList[i].GetComponent<F_Tile>().airQuality;
            waterQualityAmount += landTileList[i].GetComponent<F_Tile>().waterQuality;
        }

        for (int i = 0; i < forestTileList.Length; i++)
        {
            landQualityAmount += forestTileList[i].GetComponent<F_Tile>().landQuality;
            airQualityAmount += forestTileList[i].GetComponent<F_Tile>().airQuality;
            waterQualityAmount += forestTileList[i].GetComponent<F_Tile>().waterQuality;
        }

        // Calculate percentage quality
        landQualityAmount = landQualityAmount / (100 * (landTileCount + forestTileCount)) * 100;
        waterQualityAmount = waterQualityAmount / (100 * (landTileCount + forestTileCount)) * 100;
        airQualityAmount = airQualityAmount / (100 * (landTileCount + forestTileCount)) * 100;
        // Clamping it between 100 & 0
        landQualityAmount = Mathf.Clamp(landQualityAmount, 0, 100);
        waterQualityAmount = Mathf.Clamp(waterQualityAmount, 0, 100);
        airQualityAmount = Mathf.Clamp(airQualityAmount, 0, 100);
        // Truncating to 2 decimal places
        landQualityAmount = Mathf.Round((landQualityAmount) * 100) / 100;
        waterQualityAmount = Mathf.Round(waterQualityAmount * 100) / 100;
        airQualityAmount = Mathf.Round(airQualityAmount * 100) / 100;

    }

    public void TotalObjectImpact()
    {
        houses = 0;
        food = 0;

        for(int i=0;i<landTileCount;i++)
        {
            GameObject ob = landTileList[i].GetComponent<F_Tile>().constructedOb;

            if(ob!=null)
            {
                landTileList[i].GetComponent<F_Tile>().airQuality += ob.GetComponent<ObjectImpact>().airQuality;
                landTileList[i].GetComponent<F_Tile>().waterQuality += ob.GetComponent<ObjectImpact>().waterQuality;
                landTileList[i].GetComponent<F_Tile>().landQuality += ob.GetComponent<ObjectImpact>().landQuality;

                economy += ob.GetComponent<ObjectImpact>().economyPts;
                food += ob.GetComponent<ObjectImpact>().foodPts;
                if(ob.tag=="House")
                {
                    houses++;
                }

            }            
        }

        for (int i = 0; i < forestTileCount; i++)
        {
            GameObject ob = forestTileList[i].GetComponent<F_Tile>().constructedOb;

            if(ob!=null)
            {
                if(ob.tag!="Dummy")
                {
                    forestTileList[i].GetComponent<F_Tile>().airQuality += ob.GetComponent<ObjectImpact>().airQuality;
                    forestTileList[i].GetComponent<F_Tile>().waterQuality += ob.GetComponent<ObjectImpact>().waterQuality;
                    forestTileList[i].GetComponent<F_Tile>().landQuality += ob.GetComponent<ObjectImpact>().landQuality;

                    economy += ob.GetComponent<ObjectImpact>().economyPts;
                    food += ob.GetComponent<ObjectImpact>().foodPts;
                    if (ob.tag == "House")
                    {
                        houses++;
                    }
                }
            }

            // Add flora & fauna params later on
        }
    }

    public void PopulateForests()
    {
        for(int i=0;i<forestTileList.Length;i++)
        {
            Vector3 pos = forestTileList[i].transform.position;
            pos.y = 1.0f;
            forestTileList[i].GetComponent<F_Tile>().constructedOb = Instantiate(ForestPrefab, position: pos, forestTileList[i].transform.rotation);
        }
    }
}
