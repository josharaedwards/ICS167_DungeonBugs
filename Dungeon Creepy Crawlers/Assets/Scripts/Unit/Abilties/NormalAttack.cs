//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Normal Attack")]
public class NormalAttack : AbilityData
{
    public override void Execute(StatsTracker caster, StatsTracker target, Ability instance) 
    {
        target.DamageCalc(instance.damage, instance.abilityType);
    }
}
