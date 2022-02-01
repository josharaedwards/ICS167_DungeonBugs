using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heavy Attack")]
public class HeavyAttack : Ability
{
    public override void Execute(StatsTracker target)
    {
        target.DamageCalc(damage);
    }
}
