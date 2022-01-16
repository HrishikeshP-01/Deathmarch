using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTile : MonoBehaviour
{
    Renderer rend;
    public Material[] hovered, selected;
    bool isSelected;

    private Material[] notSelected;

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
        TileSelected();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1) && isSelected == true)
        {
            TileDeSelected();
        }
    }

    void TileSelected()
    {
        isSelected = true;
        rend.materials = selected;

    }

    void TileDeSelected()
    {
        isSelected = false;
        rend.materials = notSelected;
    }
}
