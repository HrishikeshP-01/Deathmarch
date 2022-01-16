using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class F_Tile : MonoBehaviour
{
    Renderer rend;
    public Material[] hovered, selected;
    bool isSelected;
    public string tileType;
    public GameObject gameController;

    private Material[] notSelected;

    private float waterPollution = 0, landPollution = 0, airPollution = 0;

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

    float GetAirPollution()
    {
        return airPollution;
    }

    float GetLandPollution()
    {
        return landPollution;
    }

    float GetWaterPollution()
    {
        return waterPollution;
    }
}
