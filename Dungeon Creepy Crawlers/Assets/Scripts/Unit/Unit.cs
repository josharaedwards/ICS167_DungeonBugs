using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unit", menuName = "Unit")]
public class Unit : ScriptableObject
{
    public string type; //type of unit 

    public Sprite miniSprite;
    public Sprite selectedSprite;
    public Sprite fullSprite;

    public Ability[] abilities;

    //stats
    public int str;
    public int def;
    public int res;

    public int movement;
}
