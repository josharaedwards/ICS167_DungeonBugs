using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovementPlayer : Movement
{   
    public override SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        if (!validMoveCellPos.Contains(cellPos))
        {
            return CallBackDeselect(); // Deselect happens
        }
        
        if (movable)
        {
            bool t = gridManager.MoveObject(gameObject, cellPos);
            if (t)
                Move(cellPos);

            return selectionHandler;
        }

        return CallBackDeselect();
    }


    public override void Move(Vector3Int cellPos)
    {
        base.Move(cellPos);

        //Joshara: Update so that AP point will be subtracted based on how many squares the player moved
        GameManager.GetInstance().UpdateAPBar(1);

    }
}

