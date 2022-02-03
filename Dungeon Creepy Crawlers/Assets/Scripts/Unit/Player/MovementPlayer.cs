// Dien Nguyen

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
}

