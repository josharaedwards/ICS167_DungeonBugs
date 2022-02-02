using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal")]
public class Heal : Ability
{
    public override void Execute(StatsTracker target)
    {
        target.DamageCalc(damage);
    }
}
