using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// Might Make this abstract in the future to make a class for manage movement range display and ability range display
public class GridGenerator : MonoBehaviour 
{

    [SerializeField] private RangeTile _tilePrefab;
    private Dictionary<Type, Dictionary<Vector3Int, RangeTile>> tileTypeDict;
    private GridManager gridManager = null;

    
    // Generate visualizing Grid and operate on them based on the type of the requesting object
    public void GenerateGrid(HashSet<Vector3Int> cellPosSet, object obj)
    {
        Type type = obj.GetType();
        Dictionary<Vector3Int, RangeTile> tileDict;

        if (!tileTypeDict.ContainsKey(type))
        {
            tileDict = new Dictionary<Vector3Int, RangeTile>();
            tileTypeDict.Add(type, tileDict);
        }
        else
        {
            tileDict = tileTypeDict[type];
        }

        foreach (Vector3Int pos in cellPosSet)
        {
            Vector3 worldPos = gridManager.cellToWorld(pos);
            RangeTile spawnedTile = Instantiate(_tilePrefab, worldPos + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity);
            spawnedTile.transform.SetParent(gameObject.transform, true);
            spawnedTile.name = $"Tile {pos.x} {pos.y}";
            tileDict.Add(pos, spawnedTile);
        }
    }

    public void DestroyGrid(object obj)
    {
        Type type = obj.GetType();
        if (!tileTypeDict.ContainsKey(type))
            return;
        Dictionary<Vector3Int, RangeTile>  tileDict = tileTypeDict[type];

        foreach (RangeTile tile in tileDict.Values)
        {
            Destroy(tile.gameObject);
        }
        tileDict.Clear();
        tileTypeDict.Remove(type);
        //Debug.Log("DESTROYED TILES");
    }

    public void HideGrid(object obj)
    {
        Type type = obj.GetType();
        if (!tileTypeDict.ContainsKey(type))
            return;
        Dictionary<Vector3Int, RangeTile> tileDict = tileTypeDict[type];

        foreach (RangeTile tile in tileDict.Values)
        {
            tile.gameObject.SetActive(false);
        }
    }

    public void ShowGrid(object obj)
    {
        Type type = obj.GetType();
        if (!tileTypeDict.ContainsKey(type))
            return;
        Dictionary<Vector3Int, RangeTile> tileDict = tileTypeDict[type];

        foreach (RangeTile tile in tileDict.Values)
        {
            tile.gameObject.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gridManager = GridManager.GetInstance();
        tileTypeDict = new Dictionary<Type,Dictionary<Vector3Int, RangeTile>>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
