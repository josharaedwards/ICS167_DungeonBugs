using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public Unit unit;

    public int hp;
    public int maxHP;

    public Ability[] abilities;

    public int str;
    public int def;
    public int res;

    public int movement;

    public HealthBarDisplay healthBar;

    void Start()
    {
        hp = unit.hp;
        maxHP = hp;
        str = unit.str;
        def = unit.def;
        res = unit.res;
        movement = unit.movement;
        abilities = unit.abilities;

        SetupHealthBar();  
    }

    //Joshara: For debug reasons only. Delete this once damaging gets hooked up!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DamageCalc(-2);
        }
    }

    public void DamageCalc(int dmg)
    {
        int dmgCalc = dmg;

        if (dmg < 0)
        {
            dmgCalc += res;

            hp += dmgCalc;
            if (hp < 0)
            {
                hp = 0;
            }
        }
        else
        {
            hp += dmgCalc;
        }

        if(healthBar)
        {
            healthBar.DamageReceived(dmgCalc);
        }
    }

    public void SetupHealthBar()
    {
        healthBar = GetComponentInChildren<HealthBarDisplay>();

        if(healthBar)
        {
            healthBar.Init(hp);
        } 
    }
}
