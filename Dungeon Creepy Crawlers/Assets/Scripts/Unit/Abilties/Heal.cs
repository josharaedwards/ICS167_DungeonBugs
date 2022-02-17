//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Heal")]
public class Heal : Ability
{
    public override void Execute(StatsTracker caster, StatsTracker target)
    {
        target.DamageCalc(damage, abilityType);
        Instantiate(particleSystem, target.transform);
        //particleSystem.transform = target.transform;
        particleSystem.Play();
    }
}
