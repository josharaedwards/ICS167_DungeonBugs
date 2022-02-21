//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    private static AbilityManager instance;
    private GridManager gridManager;

    private void Start()
    {
        gridManager = GridManager.GetInstance();
    }

    private void Awake()
    {
        instance = this;
    }

    public static AbilityManager GetInstance() 
    {
        return instance;
    }

    public bool InRange(StatsTracker caster, StatsTracker target, Ability ability) 
    {
        Vector3Int casterPos = gridManager.GetPosFromObject(caster.gameObject);
        Vector3Int targetPos = gridManager.GetPosFromObject(target.gameObject);
        if (Vector3Int.Distance(casterPos, targetPos) > ability.minRange-0.9 & Vector3Int.Distance(casterPos, targetPos) <= ability.range) // -0.9 in case of diagonal
        {
            return true;
        }
        else {
            return false;
        }
    }

    public bool Cast(StatsTracker caster, StatsTracker target, Ability ability) 
    {
        if (InRange(caster, target, ability)) 
        {
            ability.Execute(caster, target);
            return true;
        }
        return false;
    }
}
