// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : Movement
{
    private AILogic mainLogic;

    [SerializeField] private int wanderRange;

    private HashSet<Vector3Int> wanderTiles;

    protected override void Start()
    {
        base.Start();
        mainLogic = GetComponent<AILogic>();

        wanderTiles = new HashSet<Vector3Int>();
        GenerateWanderTiles();

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

    public void Pursue()
    {
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(mainLogic.CurrentTarget());
        Vector3Int nextPos;
        nextPos = GetNextMove(currentTargetPos);
        Move(nextPos);
    }

    public void RunAway()
    {
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(mainLogic.CurrentTarget());
        Vector3Int nextPos;
        nextPos = GetRunAway(currentTargetPos);
        Move(nextPos);
    }

    public void Wander()
    {
        int r = Random.Range(0, wanderTiles.Count);
        int i = 0;
        Vector3Int currentTargetPos = currentCellPos;
        foreach (Vector3Int tile in wanderTiles) // Randomly choose a tile in wanderTiles to wander towards
        {
            if (i==r)
            {
                currentTargetPos = tile;
                break;
            }
            i++;
        }

        Vector3Int nextPos;
        nextPos = GetNextMove(currentTargetPos);
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


    //TODO: Combine the generate tiles functions to one helper function
    public void GenerateWanderTiles()
    {
        wanderTiles.Clear();
        // a temporary set to store the validMove to the next loop to calculate the next valid move pos
        HashSet<Vector3Int>[] tempPos = new HashSet<Vector3Int>[wanderRange + 1];
        Vector3Int[] fourDirections = { Vector3Int.right, Vector3Int.left, Vector3Int.up, Vector3Int.down };

        for (int i = 0; i <= wanderRange; ++i)
        {
            tempPos[i] = new HashSet<Vector3Int>();
        }

        tempPos[0].Add(currentCellPos);
        for (int i = 1; i <= wanderRange; ++i)
        {
            foreach (Vector3Int pos in tempPos[i - 1])
            {
                foreach (Vector3Int direction in fourDirections)
                {
                    tempPos[i].Add(pos + direction);
                }
            }
            foreach (Vector3Int pos in tempPos[i])
            {
                wanderTiles.Add(pos);
            }
        }
    }
}
