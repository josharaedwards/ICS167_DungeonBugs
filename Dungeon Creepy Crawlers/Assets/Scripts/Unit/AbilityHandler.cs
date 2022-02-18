// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityHandler : MonoBehaviour, InputSelectReceiver
{
    [SerializeField] private Color visualColor;

    protected Ability[] abilities;
    protected StatsTracker statsTracker;

    protected Ability selectedAbility;

    protected GridGenerator gridGenerator;
    protected Movement movementComp;

    protected SelectionHandler selectionHandler;

    protected GridManager gridManager;

    protected bool castable;

    protected HashSet<Vector3Int> rangeVisual;
    protected HashSet<Vector3Int> areaVisual;
    protected bool prevMovabable; // Store object Movability before disabling it for ability usage

    protected virtual void Start()
    {
        gridGenerator = GetComponent<GridGenerator>();
        movementComp = GetComponent<Movement>();

        selectionHandler = GetComponent<SelectionHandler>();
        selectionHandler.Subscribe(this);

        gridManager = GridManager.GetInstance();

        statsTracker = GetComponent<StatsTracker>();
        abilities = statsTracker.GetInitAbilities();
        

        rangeVisual = new HashSet<Vector3Int>();
        areaVisual = new HashSet<Vector3Int>();

        castable = true; // By default the player goes first, might change this later
    }

    public void Select(Ability ability)  // Will be called by an ability button
    {
        if (ability == selectedAbility)
        {
            return;
        }
        selectedAbility = ability;
        prevMovabable = movementComp.Movable();

        movementComp.DisableMovement(); //Disable movement so unit dont try to move while using ability

        VisualizeRange();
        VisualizeArea();

    }

    public void Deselect()
    {
        CallBackDeselect();
    }

    public virtual SelectionHandler CallBackSelect()
    {
        return selectionHandler;
    }

    public virtual SelectionHandler CallBackSelect(Vector3Int targetPos)
    {
        return null;
    }

    public virtual SelectionHandler CallBackDeselect()
    {
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
        gridGenerator.GenerateGrid(rangeVisual, this, visualColor);
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

    public Ability[] GetAbilities()
    {
        return abilities;
    }
}
