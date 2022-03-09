//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Normal Ranged")]
public class NormalRanged : AbilityData
{
    public override void Execute(StatsTracker caster, StatsTracker target, Ability instance)
    {
        base.Execute(caster, target, instance);
        target.DamageCalc(instance.damage, instance.abilityType);
    }
}
