using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileActions : MonoBehaviour
{
    public GameObject selectedTile;
    public GameObject selectedPrefab;

    public float prefabYOffset = 1.0f;

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
        Debug.Log(selectedTile);
    }

    public void TileDeSelected(GameObject deselected)
    {
        if(selectedTile!=null)
        {
            selectedTile.GetComponent<F_Tile>().TileDeSelected();
        }
        selectedTile = null;
        Debug.Log("Deselected");
    }

    public void PlaceItem(GameObject selected)
    {
        selectedPrefab = selected;
        if (selectedTile!=null)
        {
            Vector3 pos = selectedTile.GetComponent<F_Tile>().gameObject.transform.position;
            pos.y = prefabYOffset;
            Destroy(selectedTile.GetComponent<F_Tile>().constructedOb);
            selectedTile.GetComponent<F_Tile>().constructedOb = Instantiate(selectedPrefab, position: pos, selectedTile.GetComponent<F_Tile>().gameObject.transform.rotation);

            Debug.Log("Done");
        }
    }
}
