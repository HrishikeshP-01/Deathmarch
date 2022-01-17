using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldUI : MonoBehaviour
{
    public GameObject gameController;
    public GameObject water, land, air, economy, food, house;

    // Using Geometric Progression for houses (populataion)
    float house_a = 2, house_r = 2;
    float houseTarget = 1;
    float foodTarget = 1, foodPerHouse = 2;

    // Start is called before the first frame update
    void Start()
    {
        water.GetComponent<Text>().text = "100";
        land.GetComponent<Text>().text = "100";
        air.GetComponent<Text>().text = "100";

        houseTarget = house_a;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateParameters()
    {
        gameController.GetComponent<EnvironmentAnalyser>().getQuality();
        water.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().waterQualityAmount.ToString();
        land.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().landQualityAmount.ToString();
        air.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().landQualityAmount.ToString();

        UpdatePopulationNeeds();

        food.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().food.ToString() + " / " + foodTarget.ToString();
        economy.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().economy.ToString();
        house.GetComponent<Text>().text = gameController.GetComponent<EnvironmentAnalyser>().houses.ToString() + " / " + houseTarget.ToString();
    }

    public void UpdatePopulationNeeds()
    {
        houseTarget += house_r;
        foodTarget = houseTarget * foodPerHouse;
    }   
}
