// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour, InputSelectReceiver, GridMovementEventReceiver
{
    protected SelectionHandler selectionHandler;
    
    protected GridGenerator gridGenerator;

    protected GridManager gridManager;

    [SerializeField] protected Vector3Int currentCellPos;
    [SerializeField] protected bool movable;
    protected bool recentlyMoved;

    protected int frame; // WILL PROBABLY DELETE THIS; JUST A TEMPORARY SOLUTION FOR RUNNING A FUNCTION ON FRAME 2


    [SerializeField] protected int movement;
    protected HashSet<Vector3Int> validMoveCellPos;

    [SerializeField] protected Vector2Int spawnPos = Vector2Int.zero;


    // Start is called before the first frame update
    protected virtual void Start()
    {


        gridGenerator = GetComponent<GridGenerator>();

        selectionHandler = GetComponent<SelectionHandler>();
        selectionHandler.Subscribe(this);

        gridManager = GridManager.GetInstance();
        validMoveCellPos = new HashSet<Vector3Int>();

        Init(spawnPos);

        movable = true;

        

        frame = 0;
    }

    public void SetMovementSpeed(int speed) // Use this to set movement from StatsTracker
    {
        movement = speed;
    }

    public void Init(Vector2Int spawnPos)
    {
        Vector3Int spawnPosVec3 = new Vector3Int(spawnPos.x, spawnPos.y, 0);
        currentCellPos = gridManager.AddObject(gameObject, spawnPosVec3);
        transform.position = gridManager.cellToWorld(currentCellPos);
        transform.position = transform.position + new Vector3(0.5f, 0.5f, 0);
    }

    public void DisableMovement()
    {
        movable = false;
    }

    public void EnableMovement()
    {
        movable = true;
    }

    public bool Movable()
    {
        return movable;
    }

    public Vector3Int currentPos()
    {
        return currentCellPos;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: USE COROUTINE TO SOVLE THIS
        if (frame <= 2) // Call GenerateValidMoveGrid() on 2nd frame with everything in the scene set up, TEMPORARY
        {
            frame++;
            if (frame >= 2)
            {
                GenerateValidMoveGrid();
            }
        }
    }

    // Re-generate valid movable tile if prevPos or newPos from the event is within the existing valid moves
    public void GridMovementEventCallBack(Vector3Int prevPos, Vector3Int newPos)
    {
        if (recentlyMoved)
        {
            recentlyMoved = false;
            return;
        }
        // Check if the newPos and prevPos is anywhere within the unit's possible range of movement
        float prevDist = Vector3Int.Distance(currentCellPos, prevPos);
        float newDist = Vector3Int.Distance(currentCellPos, newPos);
        if ((int) newDist <= movement || (int) prevDist <= movement)
        {
            GenerateValidMoveGrid();
        }
    }

    protected void GenerateValidMoveGrid()
    {
        gridGenerator.DestroyGrid(this);

        GenerateValidMove();
        gridGenerator.GenerateGrid(validMoveCellPos, this);
        gridGenerator.HideGrid(this);
    }

    // Generate valid movement each turn
    protected void GenerateValidMove() // TODO: Consider ally that could be went through
    {
        validMoveCellPos.Clear();
        // a temporary set to store the validMove to the next loop to calculate the next valid move pos
        HashSet<Vector3Int>[] tempPos = new HashSet<Vector3Int>[movement + 1];
        Vector3Int[] fourDirections = { Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down };

        Vector3Int t; // a temp variable to check gridManager.IsOccupied(t)

        for (int i = 0; i <= movement; ++i)
        {
            tempPos[i] = new HashSet<Vector3Int>();
        }

        tempPos[0].Add(currentCellPos);
        for (int i = 1; i <= movement; ++i)
        {
            foreach (Vector3Int pos in tempPos[i - 1])
            {
                foreach (Vector3Int direction in fourDirections)
                {
                    t = pos + direction;
                    if (!gridManager.IsOccupied(t)) // check if t is occupied 
                    {
                        tempPos[i].Add(t);
                    }
                } 
            }
            foreach (Vector3Int pos in tempPos[i])
            {
                validMoveCellPos.Add(pos);
            }
        }
    }


    public virtual SelectionHandler CallBackSelect()
    {
        /*if (turnEventHandler.turn == GameManager.TurnState.PlayerTurn)
            Debug.Log("SELECTED PLAYER");
        else
            Debug.Log("SELECTED ENEMY");*/
        gridGenerator.ShowGrid(this);
        return selectionHandler;
    }

    public virtual SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        return null;

    }

    public virtual SelectionHandler CallBackDeselect()
    {
        /*if (turnEventHandler.turn == GameManager.TurnState.PlayerTurn)
            Debug.Log("DESELECTED PLAYER");
        else
            Debug.Log("DESELECTED ENEMY");*/
        gridGenerator.HideGrid(this);
        return null;
    }


    public virtual bool Move(Vector3Int cellPos)
    {
        if (!movable)
            return false;
        recentlyMoved = true;
        bool t = gridManager.MoveObject(gameObject, cellPos); // Check if its movable on grid
        if (!t)
        {
            recentlyMoved = false;
            return false;
        }

        currentCellPos = cellPos;
        transform.position = gridManager.cellToWorld(currentCellPos);
        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);

        GenerateValidMoveGrid();
        DisableMovement();

        return true;
    }

    IEnumerator FrameDelay(int n)
    {
        for (int i = 0; i < n; i++)
        {
            yield return null;
        }
    }
}
