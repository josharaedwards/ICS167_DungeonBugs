using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementPlayer : Movement
{   
    public override SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        if (!validMoveCellPos.Contains(cellPos))
        {
            return null; // Deselect happens
        }

        bool t = Move(cellPos);
        if (t)
        {
            return selectionHandler;
        }

        return null;
    }


    public override bool Move(Vector3Int cellPos)
    {
        bool t = base.Move(cellPos);

        //Joshara: Update so that AP point will be subtracted based on how many squares the player moved
        if (!t)
        {
            return false;
        }

        return true;


    }
}

