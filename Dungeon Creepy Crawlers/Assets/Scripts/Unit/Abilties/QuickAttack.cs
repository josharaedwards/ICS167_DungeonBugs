//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quick Attack")]
public class QuickAttack : AbilityData
{
    public override void Execute(StatsTracker caster, StatsTracker target, Ability instance)
    {
        target.DamageCalc(instance.damage, instance.abilityType);
    }
}
