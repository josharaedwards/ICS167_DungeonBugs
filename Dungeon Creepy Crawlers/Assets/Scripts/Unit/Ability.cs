//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : ScriptableObject
{
    public string abilityName;
    public string description;
    public AbilType abilityType;
    public ParticleSystem particleSystem;

    public bool teamCast; //for support abilities
    public bool areaOfEffect; //if it can hit multiple units
    public enum AbilType { Phys, Mag, Buff };

    public int damage;
    public int minRange;
    public int range;
    public int apCost;

    public abstract void Execute(StatsTracker caster, StatsTracker target);
}
