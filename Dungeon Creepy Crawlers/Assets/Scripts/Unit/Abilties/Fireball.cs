using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fireball")]
public class Fireball : Ability
{
    public override void Execute(StatsTracker target)
    {
        target.DamageCalc(damage);
    }
}
