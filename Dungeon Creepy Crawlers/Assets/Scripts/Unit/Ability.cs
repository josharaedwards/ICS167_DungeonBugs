using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    private AbilityData abilityData;

    public string abilityName;
    public string description;
    public AbilType abilityType;
    public ParticleSystem particleSystem;

    public bool teamCast; //for support abilities
    public bool areaOfEffect; //if it can hit multiple units

    public int damage;
    public int minRange;
    public int range;
    public int apCost;

    public Ability(AbilityData _abilityData)
    {
        abilityData = _abilityData;

        abilityName = _abilityData.abilityName;
        description = _abilityData.description;
        abilityType = _abilityData.abilityType;
        particleSystem = _abilityData.particleSystem;

        teamCast = _abilityData.teamCast;
        areaOfEffect = _abilityData.areaOfEffect;
        
        damage = _abilityData.damage;
        minRange = _abilityData.minRange;
        range = _abilityData.range;
        apCost = _abilityData.apCost;
    }

    public void Execute(StatsTracker caster, StatsTracker target)
    {
        abilityData.Execute(caster, target, this);
    }
}
