// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : Movement
{
    private AILogic mainLogic;


    protected override void Start()
    {
        base.Start();
        mainLogic = GetComponent<AILogic>();

    }

    public void MoveNext(bool casted)
    {
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(mainLogic.CurrentTarget());
        Vector3Int nextPos;
        if (casted)
        {
            nextPos = GetRunAway(currentTargetPos);
        }
        else
        {
            nextPos = GetNextMove(currentTargetPos);
        }
        Move(nextPos);
    }

    private Vector3Int GetRunAway(Vector3Int currentTargetPos) // Run away from target
    {
        Vector3Int runAwayPos = currentCellPos - currentTargetPos + currentCellPos; // get a position mirrored the targetPos
        return GetNextMove(runAwayPos);
    }

    private Vector3Int GetNextMove(Vector3Int currentTargetPos) // Simple Greedy Pathfinding toward the target
    {
        // TODO: When ability is implemented include the logic of using them
        float distanceToTarget = float.MaxValue;
        float tempDist;
        Vector3Int bestMove = Vector3Int.zero;
        foreach (Vector3Int move in validMoveCellPos)
        {
            tempDist = Vector3Int.Distance(move, currentTargetPos);
            if (tempDist < distanceToTarget)
            {
                distanceToTarget = tempDist;
                bestMove = move;
            }
        }
        return bestMove;
    }
}
