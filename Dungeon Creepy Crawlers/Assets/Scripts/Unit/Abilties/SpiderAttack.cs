//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spider Attack")]
public class SpiderAttack : AbilityData
{
    public override void Execute(StatsTracker caster, StatsTracker target, Ability instance) //if we make this AoE it will have to have a list of args
    {
        base.Execute(caster, target, instance);
        target.DamageCalc(instance.damage, instance.abilityType);
    }
}
 