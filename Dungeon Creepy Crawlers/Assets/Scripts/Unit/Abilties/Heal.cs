//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal")]
public class Heal : Ability
{
    public override void Execute(StatsTracker caster, StatsTracker target)
    {
        base.Execute(caster, target);
        target.DamageCalc(damage, abilityType);
    }
}
