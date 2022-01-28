using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Might Make this abstract in the future to make a class for manage movement range display and ability range display
public class GridGenerator : MonoBehaviour 
{

    [SerializeField] private RangeTile _tilePrefab;
    private Dictionary<Vector3Int, RangeTile> tileDict;
    private GridManager gridManager = null;

    /*void GenerateGrid(int _width, int _height)
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                RangeTile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                _tileList[x,y] = spawnedTile;

                //bool offSet = (x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0);
                spawnedTile.Init();

            }
        }

        //_camera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10); // Move camera so 0,0 is at the bottom left
    }*/

    public void GenerateGrid(HashSet<Vector3Int> cellPosSet)
    {
        foreach (Vector3Int pos in cellPosSet)
        {
            Vector3 worldPos = gridManager.cellToWorld(pos);
            RangeTile spawnedTile = Instantiate(_tilePrefab, worldPos + new Vector3(0.5f,0.5f,0f), Quaternion.identity);
            spawnedTile.transform.SetParent(gameObject.transform, true);
            spawnedTile.name = $"Tile {pos.x} {pos.y}";
            tileDict.Add(pos, spawnedTile);
        }
    }

    public void DestroyGrid()
    {
        foreach (RangeTile tile in tileDict.Values)
        {
            Destroy(tile.gameObject);
        }
        tileDict.Clear();
        //Debug.Log("DESTROYED TILES");
    }

    public void HideGrid()
    {
        foreach (RangeTile tile in tileDict.Values)
        {
            tile.gameObject.SetActive(false);
        }
    }

    public void ShowGrid()
    {
        foreach (RangeTile tile in tileDict.Values)
        {
            tile.gameObject.SetActive(true);
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        gridManager = GridManager.GetInstance();
        tileDict = new Dictionary<Vector3Int, RangeTile>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
