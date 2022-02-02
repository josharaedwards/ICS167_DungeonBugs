using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private List<GameObject> players;

    private static PlayerManager instance;

    [SerializeField] private int ActionPoint;



    public static PlayerManager GetInstance()
    {
        return instance;
    }

    void Awake()
    {
        instance = this;
    }

    void Start()
    {

    }

    public GameObject[] GetPlayers()
    {
        return players.ToArray();
    }

    public int GetActionPoint()
    {
        return ActionPoint;
    }
}
