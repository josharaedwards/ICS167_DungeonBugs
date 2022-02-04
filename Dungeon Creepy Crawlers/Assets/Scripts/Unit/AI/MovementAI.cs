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

    public void MoveNext()
    {
        Vector3Int T = GetNextMove();
        Move(T);
    }

    private Vector3Int GetNextMove() // Simple Greedy Pathfinding toward the target
    {
        // TODO: When ability is implemented include the logic of using them
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(mainLogic.CurrentTarget());
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
