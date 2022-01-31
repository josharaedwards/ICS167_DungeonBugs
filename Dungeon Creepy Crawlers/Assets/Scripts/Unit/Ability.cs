using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]
public class Ability : ScriptableObject
{
    public string abilityName;
    public string description;

    public bool teamCast; //for support abilities
    public bool areaOfEffect; //if it can hit multiple units

    public int damage;
    public int range;
    public int apCost;
}
