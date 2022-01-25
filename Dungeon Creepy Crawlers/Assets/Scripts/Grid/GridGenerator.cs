using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int _width, _height;

    [SerializeField] private RangeTile _tilePrefab;
    private RangeTile[,] _tileList;

    [SerializeField] private Camera _camera;

    void GenerateGrid()
    {
        for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                RangeTile spawnedTile = Instantiate(_tilePrefab, new Vector3(x, y), Quaternion.identity);
                spawnedTile.name = $"Tile {x} {y}";
                _tileList[x,y] = spawnedTile;

                bool offSet = (x % 2 == 0 && y % 2 == 1) || (x % 2 == 1 && y % 2 == 0);
                spawnedTile.Init(offSet);


            }
        }

        _camera.transform.position = new Vector3((float)_width / 2 - 0.5f, (float)_height / 2 - 0.5f, -10); // Move camera so 0,0 is at the bottom left
    }
    // Start is called before the first frame update
    void Start()
    {
        _tileList = new RangeTile[_width,_height];
        GenerateGrid();
    }

    public RangeTile GetTileAtPos (int x, int y)
    {
        if (x < _width && y < _height && x >= 0 && y >= 0)
        {
            return _tileList[x, y];
        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
