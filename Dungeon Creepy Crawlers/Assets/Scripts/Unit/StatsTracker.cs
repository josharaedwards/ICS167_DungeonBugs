using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public Unit unit;

    public int hp;

    public Ability[] abilities;

    public int str;
    public int def;
    public int res;

    public int movement;

    void Start()
    {
        hp = unit.hp;
        str = unit.str;
        def = unit.def;
        res = unit.res;
        movement = unit.movement;
        abilities = unit.abilities;
    }

    public void DamageCalc(int dmg)
    {
        if (dmg < 0)
        {
            hp += dmg + res;
            if (hp < 0)
            {
                hp = 0;
            }
        }
        else
        {
            hp += dmg;
        }
    }
}
