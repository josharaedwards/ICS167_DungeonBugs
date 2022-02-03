using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHandler : MonoBehaviour, InputSelectReceiver
{
    private Ability selectedAbility;

    private GridGenerator gridGenerator;
    private MovementPlayer movementComp;
    private SelectionHandler selectionHandler;

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

        playerManager = PlayerManager.GetInstance();

        rangeVisual = new HashSet<Vector3Int>();
        areaVisual = new HashSet<Vector3Int>();
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
        if (selectedAbility == null)
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
        playerManager.Cast(GetComponent<StatsTracker>(), targetStatsTracker, selectedAbility);
        return null;

    }

    public SelectionHandler CallBackDeselect()
    {
        if (prevMovabable) // Return movability to before
        {
            movementComp.EnableMovement();
        }

        if (selectedAbility != null)
        {
            selectedAbility = null;
            gridGenerator.DestroyGrid(this);
            return selectionHandler;
        }
        return null;
    }

    private void VisualizeRange()
    {
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

    private void VisualizeArea()
    {

    }

}
