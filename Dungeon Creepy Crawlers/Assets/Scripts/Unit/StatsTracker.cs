// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct stats
{
    public int hp;
    public int maxHP;

    public int str;
    public int def;
    public int res;

    public int movement;
}

public class StatsTracker : MonoBehaviour
{
    public Unit unit;

    public string type;

    public Sprite miniSprite;
    public Sprite selectedSprite;
    public Sprite fullSprite;

    public Ability[] abilities;

    private stats unitStats;

    private HealthBarDisplay healthBar;

    private PlayerManager playerManager;

    private AbilityHandler abilityHandler;

    private SpriteRenderer spriteRenderer;

    private Movement movementInst;

    void Awake()
    {
        UpdateUnit();
    }

    void Start()
    {
        if (gameObject.tag == "Player")
        {
            playerManager = PlayerManager.GetInstance();
            playerManager.Add(this);
        }

        SetupHealthBar();

        abilityHandler = GetComponent<AbilityHandler>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        movementInst = GetComponent<Movement>();

        movementInst.SetMovementSpeed(unitStats.movement);
        spriteRenderer.sprite = miniSprite;

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
            dmgCalc += unitStats.def;

            unitStats.hp += dmgCalc;
        }
        else if (abilType == Ability.AbilType.Mag) {
            dmgCalc += unitStats.res;

            unitStats.hp += dmgCalc;
        }
        else
        {
            unitStats.hp += dmgCalc;
        }

        if (unitStats.hp < 0)
        {
            unitStats.hp = 0;
        }

        if (unitStats.hp > unitStats.maxHP) {
            unitStats.hp = unitStats.maxHP;
        }

        if(healthBar)
        {
            healthBar.DamageReceived(dmgCalc);
        }

        if (unitStats.hp == 0)
        {
            HandleDeath();
        }
    }

    private void HandleDeath()
    {
        GameManager.GetInstance().Unsubscribe(GetComponent<TurnEventHandler>()); // Will probably move this away from TurnEventHandler
        if (playerManager != null)
        {
            playerManager.Remove(this);
        } 
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

    public void DeselectAbility()
    {
        abilityHandler.Deselect();
    }

    public void SetupHealthBar()
    {
        healthBar = GetComponentInChildren<HealthBarDisplay>();

        if(healthBar)
        {
            healthBar.Init(unitStats.hp);
        } 
    }

    private void UpdateUnit()
    {
        type = unit.type;
        miniSprite = unit.miniSprite;
        selectedSprite = unit.selectedSprite;
        fullSprite = unit.fullSprite;
        unitStats.hp = unit.hp;
        unitStats.maxHP = unitStats.hp;
        unitStats.str = unit.str;
        unitStats.def = unit.def;
        unitStats.res = unit.res;
        unitStats.movement = unit.movement;
        abilities = unit.abilities;
    }

    public Ability[] GetInitAbilities()
    {
        return unit.abilities;
    }

    public stats GetStats()
    {
        return unitStats;
    }
}
