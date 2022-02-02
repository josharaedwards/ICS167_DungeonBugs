//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quick Attack")]
public class QuickAttack : Ability
{
    public override void Execute(StatsTracker target)
    {
        target.DamageCalc(damage, abilityType);
    }
}
