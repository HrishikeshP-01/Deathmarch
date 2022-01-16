using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Tile : MonoBehaviour
{
    Renderer rend;
    public Material[] hovered, selected;
    bool isSelected;
    public GameObject gameController;
    public GameObject constructedOb;

    private Material[] notSelected;

    public float waterQuality = 100, landQuality = 100, airQuality = 100;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        notSelected = rend.materials;

    }

    void OnMouseEnter()
    {
        if (isSelected == false)
        {
            rend.materials = hovered;
        }
    }

    private void OnMouseExit()
    {
        if (isSelected == false)
        {
            rend.materials = notSelected;
        }
    }

    private void OnMouseDown()
    {
        gameController.GetComponent<SelectedTileActions>().TileSelected(gameObject);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1) && isSelected == true)
        {
            gameController.GetComponent<SelectedTileActions>().TileDeSelected(gameObject);
        }
    }

    public void TileSelected()
    {
        isSelected = true;
        rend.materials = selected;
    }

    public void TileDeSelected()
    {
        isSelected = false;
        rend.materials = notSelected;
    }
}
