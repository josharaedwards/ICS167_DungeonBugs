// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HighlightGrid : MonoBehaviour
{
    private Grid grid;
    [SerializeField] private Tilemap interactiveMap = null;
    [SerializeField] private Tile hoverTile = null;


    private Vector3Int previousMousePos = new Vector3Int();

    private static HighlightGrid Instance; // SINGLETON

    public static HighlightGrid GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        grid = gameObject.GetComponent<Grid>();
        Instance = this;
    }

    

    // Update is called once per frame
    void Update()
    {
        // Mouse over -> highlight tile
        Vector3Int mousePos = GetMousePosition();
        if (!mousePos.Equals(previousMousePos))
        {
            interactiveMap.SetTile(previousMousePos, null); // Remove old hoverTile
            interactiveMap.SetTile(mousePos, hoverTile);
            previousMousePos = mousePos;
        }
    }

    public Vector3Int GetHighlightedCellPos()
    {
        return previousMousePos;
    }

    private Vector3Int GetMousePosition()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return grid.WorldToCell(mouseWorldPos);
    }
}
