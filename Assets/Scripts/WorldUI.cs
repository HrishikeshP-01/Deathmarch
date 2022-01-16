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
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateParameters()
    {
        gameController.GetComponent<EnvironmentAnalyser>().getPollution();
        water.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().waterPollutionAmount.ToString();
        land.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().landPollutionAmount.ToString();
        air.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().landPollutionAmount.ToString();
    }
}
