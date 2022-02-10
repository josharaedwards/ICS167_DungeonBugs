// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsTracker : MonoBehaviour
{
    public Unit unit;

    public string type;

    public Sprite miniSprite;
    public Sprite selectedSprite;
    public Sprite fullSprite;

    public int hp;
    public int maxHP;

    public Ability[] abilities;

    public int str;
    public int def;
    public int res;

    public int movement;

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

        movementInst.SetMovementSpeed(movement);
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

        if (hp > maxHP) {
            hp = maxHP;
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
        playerManager.Remove(this);
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
            healthBar.Init(hp);
        } 
    }

    private void UpdateUnit()
    {
        type = unit.type;
        miniSprite = unit.miniSprite;
        selectedSprite = unit.selectedSprite;
        fullSprite = unit.fullSprite;
        hp = unit.hp;
        maxHP = hp;
        str = unit.str;
        def = unit.def;
        res = unit.res;
        movement = unit.movement;
        abilities = unit.abilities;
    }
}
