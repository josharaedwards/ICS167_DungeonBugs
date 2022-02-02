//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Normal Attack")]
public class NormalAttack : Ability
{
    public override void Execute(StatsTracker target) {
        target.DamageCalc(damage, abilityType);
    }
}
