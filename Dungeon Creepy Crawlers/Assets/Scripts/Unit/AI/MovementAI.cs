// Dien Nguyen

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementAI : Movement
{
    private AILogic mainLogic;
    private ActionQueue actionQueue;

    [SerializeField] private int wanderRange;

    private HashSet<Vector3Int> wanderTiles;

    protected override void Start()
    {
        base.Start();
        mainLogic = GetComponent<AILogic>();
        actionQueue = GetComponent<ActionQueue>();

        wanderTiles = new HashSet<Vector3Int>();
        GenerateWanderTiles();

    }

    // Add the Moving the transform to a queue to be executed by AIManager
    protected override void MoveTransform()
    {
        actionQueue.Add(MoveToTarget);
    }

    public void Pursue(GameObject target, int minRange)
    {
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(target);
        Vector3Int nextPos;
        nextPos = GetNextMove(currentTargetPos, minRange);
        Move(nextPos);
    }

    public void RunAway(GameObject target)
    {
        Vector3Int currentTargetPos = gridManager.GetPosFromObject(target);
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
        nextPos = GetNextMove(currentTargetPos, 0);
        Move(nextPos);
    }


    private Vector3Int GetRunAway(Vector3Int currentTargetPos) // Run away from target
    {
        Vector3Int runAwayPos = 10*(currentCellPos - currentTargetPos) + currentCellPos; // get the furthest position mirrored the targetPos
        return GetNextMove(runAwayPos, 0);
    }

    private Vector3Int GetNextMove(Vector3Int currentTargetPos, int minRange) // Simple Greedy Pathfinding toward the target
    {
        Vector3Int bestMove = currentCellPos;
        float distanceToTarget = Vector3Int.Distance(currentCellPos, currentTargetPos);
        float tempDist;

        foreach (Vector3Int move in validMoveCellPos)
        {   
            tempDist = Vector3Int.Distance(move, currentTargetPos);
            if (tempDist < distanceToTarget && tempDist > minRange - 0.9)
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
