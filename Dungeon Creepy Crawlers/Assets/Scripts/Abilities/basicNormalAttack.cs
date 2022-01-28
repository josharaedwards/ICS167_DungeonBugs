using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicNormalAttack : Ability
{
    public basicNormalAttack() {
        abilityName = "Attack";
        description = "Basic melee attack.";

        teamCast = false;

        damage = 3;
        range = 1;
        manaCost = 2;
    }
    
}
