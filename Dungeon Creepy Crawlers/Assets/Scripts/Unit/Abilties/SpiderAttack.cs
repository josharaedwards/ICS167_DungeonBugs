using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spider Attack")]
public class SpiderAttack : Ability
{
    public override void Execute(StatsTracker target) //if we make this AoE it will have to have a list of args
    {
        target.DamageCalc(damage);
    }
}
 