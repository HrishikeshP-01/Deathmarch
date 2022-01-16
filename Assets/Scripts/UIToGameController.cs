using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToGameController : MonoBehaviour
{
    [SerializeField]
    private GameObject housePrefab, farmPrefab, forestPrefab, factoryPrefab, selectedPrefab;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setSelectedPrefab(string ch)
    {
        switch (ch)
        {
            case "Forest":
                selectedPrefab = forestPrefab;
                break;
            case "House":
                selectedPrefab = housePrefab;
                break;
            case "Farm":
                selectedPrefab = farmPrefab;
                break;
            case "Factory":
                selectedPrefab = factoryPrefab;
                break;
        }
        gameObject.GetComponent<SelectedTileActions>().PlaceItem(selectedPrefab);
    }
}
