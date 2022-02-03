//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private GameObject unitInfoSpawnLoc;

    public static UIManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if(instance && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public Transform GetUnitSpawnLocation()
    {
        return unitInfoSpawnLoc.transform;
    }
}
