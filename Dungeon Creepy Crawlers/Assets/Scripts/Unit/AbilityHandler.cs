using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour, InputSelectReceiver, TurnEventReciever
{
    private Ability selectedAbility;

    private GridGenerator gridGenerator;
    private MovementPlayer movementComp;

    private SelectionHandler selectionHandler;
    private TurnEventHandler turnEventHandler;

    private PlayerManager playerManager;
    private GridManager gridManager;

    private bool castable;

    private HashSet<Vector3Int> rangeVisual;
    private HashSet<Vector3Int> areaVisual;
    private bool prevMovabable; // Store object Movability before disabling it for ability usage

    void Start()
    {
        gridGenerator = GetComponent<GridGenerator>();
        movementComp = GetComponent<MovementPlayer>();

        selectionHandler = GetComponent<SelectionHandler>();
        selectionHandler.Subscribe(this);

        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);

        playerManager = PlayerManager.GetInstance();
        gridManager = GridManager.GetInstance();

        rangeVisual = new HashSet<Vector3Int>();
        areaVisual = new HashSet<Vector3Int>();

        castable = true; // By default the player goes first, might change this later
    }

    public void Select(Ability ability)
    {
        selectedAbility = ability;
        prevMovabable = movementComp.Movable();

        movementComp.DisableMovement(); //Disable movement so unit dont try to move while using ability

        VisualizeRange();
        VisualizeArea();

    }

    public SelectionHandler CallBackSelect()
    {
        return selectionHandler;
    }

    public SelectionHandler CallBackSelect(Vector3Int targetPos)
    {
        if (selectedAbility == null || !castable)
        {
            return null;
        }
        
        GameObject target = gridManager.GetObjectFromCell(targetPos);
        if (target == null)
        {
            return null;
        }

        StatsTracker targetStatsTracker = target.GetComponent<StatsTracker>();
        if (targetStatsTracker == null)
        {
            return null;
        }
        bool result = playerManager.Cast(GetComponent<StatsTracker>(), targetStatsTracker, selectedAbility);
        Debug.Log(result);
        if (result)
        {
            DisableCast();
        }
        return null;

    }

    public SelectionHandler CallBackDeselect()
    {
        //Debug.Log("DeselectAbilityCalled");

        if (selectedAbility != null)
        {
            if (prevMovabable) // Return movability to before
            {
                movementComp.EnableMovement();
            }
            selectedAbility = null;
            gridGenerator.DestroyGrid(this);
            return selectionHandler;
        }
        return null;
    }

    private void VisualizeRange()
    {
        gridGenerator.DestroyGrid(this);
        GenerateRange();
        gridGenerator.GenerateGrid(rangeVisual, this);
    }

    private void GenerateRange() // Reuse Generate valid move code
    {
        rangeVisual.Clear();
        int range = selectedAbility.range;

        HashSet<Vector3Int>[] tempPos = new HashSet<Vector3Int>[range + 1];
        Vector3Int[] fourDirections = { Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down };

        for (int i = 0; i <= range; ++i)
        {
            tempPos[i] = new HashSet<Vector3Int>();
        }

        tempPos[0].Add(movementComp.currentPos());

        for (int i = 1; i <= range; ++i)
        {
            foreach (Vector3Int pos in tempPos[i - 1])
            {
                foreach (Vector3Int direction in fourDirections)
                {
                    tempPos[i].Add(pos + direction);
                }
            }
            foreach (Vector3Int pos in tempPos[i])
            {
                rangeVisual.Add(pos);
            }
        }
    }

    private void VisualizeArea() // Will be used to visualize AoE
    {

    }

    public virtual void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        if (turnState != turnEventHandler.turn)
        {
            DisableCast();
        }
        else if (turnState == turnEventHandler.turn)
        {
            EnableCast();
        }
    }

    public void EnableCast()
    {
        castable = true;
    }
    public void DisableCast()
    {
        castable = false;
    }

    public bool Castable()
    {
        return castable;
    }

}
