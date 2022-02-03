// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    private Grid grid;
    private Tilemap map;

    private HashSet<GameObject> gameObjects = new HashSet<GameObject>();

    private Dictionary<Vector3Int, GameObject> objFromCell = new Dictionary<Vector3Int, GameObject>(); // <cellPos, Object>
    private Dictionary<GameObject, Vector3Int> objPos = new Dictionary<GameObject, Vector3Int>();      // <Object, cellPos>
    /* [SerializeField] private List<TileProperty> TileProperties; 
     private Dictionary<TileBase,TileData> dataFromTiles */

    private static GridManager Instance; // SINGLETON

    public static GridManager GetInstance()
    {
        return Instance;
    }

    private void Awake()
    {
        GridManager.Instance = this;
        grid = gameObject.GetComponent<Grid>();
    }


    // Start is called before the first frame update
    void Start()
    {
        //TODO: Set up tile data dictionary
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO: Implement edge case where spawn pos is already occupied
    public Vector3Int AddObject(GameObject obj, Vector3Int cellPos)
    {
        gameObjects.Add(obj);
        objFromCell.Add(cellPos, obj);
        objPos.Add(obj, cellPos);
        return cellPos;

    }

    // Overload: worlPos is passed instead of cellPos
    public Vector3Int AddObject(GameObject obj, Vector3 worldPos)
    {
        return AddObject(obj, grid.WorldToCell(worldPos));
    }


    // Move obj to targetPos on grid
    public bool MoveObject(GameObject obj, Vector3Int targetPos)
    {
        if (IsOccupied(targetPos)) // Check if any object occupy targetPos
            return false;

        Vector3Int prevPos = objPos[obj];

        // Update pos for obj
        objFromCell.Remove(objPos[obj]);
        objPos[obj] = targetPos;
        objFromCell.Add(targetPos, obj);

        GridMovementEvent(prevPos, targetPos);

        return true;
    }

    // Tell every GridMovementEventReceiver object something just moved
    private void GridMovementEvent(Vector3Int prevPos, Vector3Int newPos)
    {
        GridMovementEventReceiver receiver = null;
        foreach (GameObject obj in gameObjects)
        {
            receiver = obj.GetComponent<GridMovementEventReceiver>();
            if (receiver != null)
            {
                receiver.GridMovementEventCallBack(prevPos, newPos);
            }
        }
    }

    public bool IsOccupied(Vector3Int cellPos)
    {
        return objFromCell.ContainsKey(cellPos);
    }


    public GameObject GetObjectFromCell(Vector3Int cellPos)
    {
        if (IsOccupied(cellPos))
            return objFromCell[cellPos];

        return null;
    }

    public Vector3Int GetPosFromObject(GameObject obj)
    {
        if (objPos.ContainsKey(obj))
        {
            return objPos[obj];
        }

        Debug.Log(obj.ToString() + " DOES NOT EXIST ON GRID");

        return new Vector3Int(1,1,1);
    }

    public Vector3 cellToWorld(Vector3Int cellPos)
    {
        return grid.CellToWorld(cellPos);
    }
}
