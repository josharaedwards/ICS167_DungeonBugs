//Jaynie Leavins

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilType { Phys, Mag, Buff };

public abstract class AbilityData : ScriptableObject
{
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

    public virtual void Execute(StatsTracker caster, StatsTracker target, Ability instance) {
        DamagePopup.Create(target.transform.root.position, damage);
        PlayAnimation(caster, target);
    }

    private void PlayAnimation(StatsTracker caster, StatsTracker target) {
        Instantiate(particleSystem, target.transform.position, Quaternion.identity);
        particleSystem.Play();
    }
}
