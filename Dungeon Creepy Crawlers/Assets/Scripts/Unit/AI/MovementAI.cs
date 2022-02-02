using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : Movement
{
    private Movement currentTarget;

    

    public bool HasTarget()
    { 
        return currentTarget != null; 
    }

    public void SetTarget(Movement target)
    {
        currentTarget = target;
    }

    public Movement CurrentTarget()
    {
        return currentTarget;
    }

    //TODO: Implement actual A* pathfinding
    public Vector3Int GetNextMove() // Simple Greedy Pathfinding toward the target
    {
        // TODO: When ability is implemented include the logic of using them

        float distanceToTarget = float.MaxValue;
        float tempDist;
        Vector3Int bestMove = Vector3Int.zero;
        foreach (Vector3Int move in validMoveCellPos)
        {
            tempDist = Vector3Int.Distance(move, currentTarget.currentPos());
            if (tempDist < distanceToTarget)
            {
                distanceToTarget = tempDist;
                bestMove = move;
            }
        }
        return bestMove;
    }
}
