//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball")]
public class Fireball : Ability
{
    public override void Execute(StatsTracker caster, StatsTracker target)
    {
        base.Execute(caster, target);
        target.DamageCalc(damage, abilityType);
    }
}
