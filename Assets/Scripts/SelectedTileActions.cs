using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileActions : MonoBehaviour
{
    public GameObject selectedTile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TileSelected(GameObject selected)
    {
        if(selectedTile!=null)
        {
            selectedTile.GetComponent<F_Tile>().TileDeSelected();
        }
        selectedTile = selected;
        selectedTile.GetComponent<F_Tile>().TileSelected();
    }

    public void TileDeSelected(GameObject deselected)
    {
        if(selectedTile!=null)
        {
            selectedTile.GetComponent<F_Tile>().TileDeSelected();
        }
        selectedTile = null;
    }
}
