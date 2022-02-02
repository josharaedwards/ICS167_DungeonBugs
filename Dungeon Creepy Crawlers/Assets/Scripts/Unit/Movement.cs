using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour, InputSelectReceiver, TurnEventReciever, GridMovementEventReceiver
{
    protected SelectionHandler selectionHandler;
    protected TurnEventHandler turnEventHandler;
    protected GridGenerator gridGenerator;

    protected GridManager gridManager;

    protected Vector3Int currentCellPos;
    protected bool movable;

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

        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);

        gridManager = GridManager.GetInstance();
        validMoveCellPos = new HashSet<Vector3Int>();

        Init(spawnPos);

        if (turnEventHandler.turn == GameManager.TurnState.PlayerTurn)
            movable = true;
        else
            movable = false;

        frame = 0;
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

    // Re-generate valid movable tile if cellPos from the event is within the existing valid moves
    public void GridMovementEventCallBack(Vector3Int cellPos)
    {
        Debug.Log("GridMovementEventCallBack");
        if (validMoveCellPos.Contains(cellPos) && cellPos != currentCellPos)
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
    protected void GenerateValidMove() // TODO: Consider obstacle that affect valid move
    {
        validMoveCellPos.Clear();
        // a temporary set to store the validMove to the next loop to calculate the next valid move pos
        HashSet<Vector3Int>[] tempPos = new HashSet<Vector3Int>[movement + 1];
        for (int i = 0; i <= movement; ++i)
        {
            tempPos[i] = new HashSet<Vector3Int>();
        }

        tempPos[0].Add(currentCellPos);

        for (int i = 1; i <= movement; ++i)
        {
            foreach (Vector3Int pos in tempPos[i - 1])
            {
                tempPos[i].Add(pos + Vector3Int.right);
                tempPos[i].Add(pos + Vector3Int.left);
                tempPos[i].Add(pos + Vector3Int.up);
                tempPos[i].Add(pos + Vector3Int.down);
            }
            foreach (Vector3Int pos in tempPos[i])
            {
                validMoveCellPos.Add(pos);
            }
        }
    }


    public virtual SelectionHandler CallBackSelect()
    {
        if (turnEventHandler.turn == GameManager.TurnState.PlayerTurn)
            Debug.Log("SELECTED PLAYER");
        else
            Debug.Log("SELECTED ENEMY");
        gridGenerator.ShowGrid(this);
        return selectionHandler;
    }

    public virtual SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        CallBackDeselect();
        return null;

    }

    public virtual SelectionHandler CallBackDeselect()
    {
        if (turnEventHandler.turn == GameManager.TurnState.PlayerTurn)
            Debug.Log("DESELECTED PLAYER");
        else
            Debug.Log("DESELECTED ENEMY");
        gridGenerator.HideGrid(this);
        return null;
    }


    public virtual void Move(Vector3Int cellPos)
    {
        currentCellPos = cellPos;
        transform.position = gridManager.cellToWorld(currentCellPos);
        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);

        GenerateValidMoveGrid();
        DisableMovement();
    }

    public virtual void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        if (turnState != turnEventHandler.turn)
        {
            DisableMovement();
        }
        else if (turnState == turnEventHandler.turn)
        {
            EnableMovement();
        }
    }
}
