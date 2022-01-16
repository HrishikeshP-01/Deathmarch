using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour
{
    public GameObject gameController;
    public GameObject water, land, air;

    // Start is called before the first frame update
    void Start()
    {
        water.GetComponent<Text>().text = "100";
        land.GetComponent<Text>().text = "100";
        air.GetComponent<Text>().text = "100";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateParameters()
    {
        gameController.GetComponent<EnvironmentAnalyser>().getQuality();
        water.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().waterQualityAmount.ToString();
        land.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().landQualityAmount.ToString();
        air.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().landQualityAmount.ToString();
    }
}
