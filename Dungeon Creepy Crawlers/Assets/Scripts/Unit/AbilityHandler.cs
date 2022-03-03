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
    protected Ability hoveredAbility;

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

        EnableCast();
    }

    public void Select(Ability ability)  // Will be called by an ability button
    {
        if (selectedAbility == null)
        {
            prevMovabable = movementComp.Movable();
            movementComp.DisableMovement(); //Disable movement so unit dont try to move while using ability
        }

        selectedAbility = ability;
        
        VisualizeRange(ability);
        VisualizeArea(ability);
    }

    public void SelectHover(Ability ability)
    {
        if (selectedAbility != null)
        {
            return;
        }

        hoveredAbility = ability;

        VisualizeRange(ability);
        VisualizeArea(ability);
    }

    public void DeselectHover()
    {
        if (selectedAbility != null)
        {
            return;
        }

        rangeVisual.Clear();
        hoveredAbility = null;
        gridGenerator.DestroyGrid(this);
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
        rangeVisual.Clear();
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

    private void VisualizeRange(Ability ability)
    {
        gridGenerator.DestroyGrid(this);
        GenerateRange(ability);
        gridGenerator.GenerateGrid(rangeVisual, this, visualColor);
    }

    private void GenerateRange(Ability ability) // Reuse Generate valid move code
    {
        rangeVisual.Clear();

        int range = ability.range;
        int minRange = ability.minRange;

        HashSet<Vector3Int>[] tempPos = new HashSet<Vector3Int>[range + 1];
        HashSet<Vector3Int> tmp = new HashSet<Vector3Int>();
        Vector3Int[] fourDirections = { Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down };

        for (int i = 0; i <= range; ++i)
        {
            tempPos[i] = new HashSet<Vector3Int>();
        }

        tempPos[0].Add(movementComp.CurrentPos());
        tmp.Add(movementComp.CurrentPos());

        for (int i = 1; i <= range; ++i)
        {
            foreach (Vector3Int pos in tempPos[i - 1])
            {
                foreach (Vector3Int direction in fourDirections)
                {
                    Vector3Int t = pos + direction;
                    if (!tmp.Contains(t))
                    {
                        tempPos[i].Add(t);
                        tmp.Add(t);
                    }
                }
            }
        }

        for (int i = minRange; i <= range; ++i)
        {
            foreach (Vector3Int pos in tempPos[i])
            {
                rangeVisual.Add(pos);
            }      
        }
    }

    private void VisualizeArea(Ability ability) // Will be used to visualize AoE
    {

    }

    public virtual void EnableCast()
    {
        castable = true;
    }
    public virtual void DisableCast()
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
