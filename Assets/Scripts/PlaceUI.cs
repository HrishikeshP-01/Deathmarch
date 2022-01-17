using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaceUI : MonoBehaviour
{
    public GameObject forestPrefab, farmPrefab, factoryPrefab, housePrefab, selectedPrefab;
    public Text waterQ, landQ, airQ, food, economy;
    public GameObject gameController;
    public GameObject itemWindow, detailWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectPrefab(string ch)
    {
        switch(ch)
        {
            case "Forest":
                selectedPrefab = forestPrefab;
                break;
            case "Farm":
                selectedPrefab = farmPrefab;
                break;
            case "Factory":
                selectedPrefab = factoryPrefab;
                break;
            case "House":
                selectedPrefab = housePrefab;
                break;
        }
        ShowDetails();
        goToDetailWindow();
    }

    public void ShowDetails()
    {
        waterQ.text = selectedPrefab.GetComponent<ObjectImpact>().waterQuality.ToString();
        landQ.text = selectedPrefab.GetComponent<ObjectImpact>().landQuality.ToString();
        airQ.text = selectedPrefab.GetComponent<ObjectImpact>().airQuality.ToString();

        food.text = selectedPrefab.GetComponent<ObjectImpact>().foodPts.ToString();
        economy.text = selectedPrefab.GetComponent<ObjectImpact>().economyPts.ToString();
    }

    public void PlaceItem()
    {
        gameController.GetComponent<SelectedTileActions>().PlaceItem(selectedPrefab);
        Debug.Log("Done");
    }

    public void goToDetailWindow()
    {
        itemWindow.SetActive(false);
        detailWindow.SetActive(true);
    }

    public void goToItemWindow()
    {
        detailWindow.SetActive(false);
        itemWindow.SetActive(true);
    }
}
