using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour, InputSelectReceiver
{
    public GameObject unitInfoPrototype;
    public GameObject spawnLocation;

    private StatsTracker unitStats;
    private SelectionHandler selectionHandler;
    private GameObject unitInfoInst;

    void Start()
    {
        selectionHandler = GetComponent<SelectionHandler>();
        selectionHandler.Subscribe(this);

        unitStats = GetComponent<StatsTracker>();
    }

    void Update()
    {
        
    }

    public SelectionHandler CallBackSelect()
    {
        Debug.Log("Open Info UI");

        InitUnitInfoDisplay();

        return selectionHandler;
    }

    public SelectionHandler CallBackDeselect()
    {
        Debug.Log("Close Info UI");

        Destroy(unitInfoInst);

        return selectionHandler;
    }

    public SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        CallBackSelect();
        return null;
    }

    private void InitUnitInfoDisplay()
    {
        if(unitStats && unitInfoPrototype)
        {
            unitInfoPrototype.GetComponent<UnitInfoDisplay>().unitStats = unitStats;
            unitInfoInst = Instantiate(unitInfoPrototype, spawnLocation.transform);
        }
        else
        {
            Debug.Log("ERROR: Missing Unit Stats or UI prototype");
        }       
    }
}
