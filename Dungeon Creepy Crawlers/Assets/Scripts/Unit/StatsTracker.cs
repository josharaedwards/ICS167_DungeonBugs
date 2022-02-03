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

    private PlayerManager playerManager;

    private AbilityHandler abilityHandler;
    private TurnEventHandler turnEventHandler;

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            playerManager = PlayerManager.GetInstance();
            playerManager.Add(this);
        }

        hp = unit.hp;
        maxHP = hp;
        str = unit.str;
        def = unit.def;
        res = unit.res;
        movement = unit.movement;
        abilities = unit.abilities;

        SetupHealthBar();

        abilityHandler = GetComponent<AbilityHandler>();
    }

    //Joshara: For debug reasons only. Delete this once damaging gets hooked up!
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DamageCalc(-2, Ability.AbilType.Phys);
        }
    }

    public void DamageCalc(int dmg, Ability.AbilType abilType)
    {
        int dmgCalc = dmg;

        if (abilType == Ability.AbilType.Phys)
        {
            dmgCalc += def;

            hp += dmgCalc;
        }
        else if (abilType == Ability.AbilType.Mag) {
            dmgCalc += res;

            hp += dmgCalc;
        }
        else
        {
            hp += dmgCalc;
        }

        if (hp < 0)
        {
            hp = 0;
        }

        if(healthBar)
        {
            healthBar.DamageReceived(dmgCalc);
        }

        if (hp == 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        GameManager.GetInstance().Unsubscribe(GetComponent<TurnEventHandler>()); // Will probably move this away from TurnEventHandler
        gameObject.SetActive(false);
    }

    public void SelectAbility(Ability ability) // Will be called by an ability button
    {
        if(ability)
        {
            abilityHandler.Select(ability);
        }
        else
        {
            Debug.Log("Missing Ability in Stats Tracker");
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
