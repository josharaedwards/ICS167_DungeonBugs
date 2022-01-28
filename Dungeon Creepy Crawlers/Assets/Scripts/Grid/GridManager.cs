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
        //TODO: Set up objFromCell, objPos
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
        if (objFromCell.ContainsKey(targetPos)) // Check if any object occupy targetPos
            return false;

        // Update pos for obj
        objFromCell.Remove(objPos[obj]);
        objPos[obj] = targetPos;
        objFromCell.Add(targetPos, obj);

        GridMovementEvent(targetPos);

        return true;
    }

    // Tell every GridMovementEventReceiver object something just moved
    private void GridMovementEvent(Vector3Int cellPos)
    {
        GridMovementEventReceiver receiver = null;
        foreach (GameObject obj in gameObjects)
        {
            receiver = obj.GetComponent<GridMovementEventReceiver>();
            if (receiver != null)
            {
                receiver.GridMovementEventCallBack(cellPos);
            }
        }
    }


    public GameObject GetObjectFromCell(Vector3Int cellPos)
    {
        if (objFromCell.ContainsKey(cellPos))
            return objFromCell[cellPos];

        return null;
    }

    public Vector3 cellToWorld(Vector3Int cellPos)
    {
        return grid.CellToWorld(cellPos);
    }
}
