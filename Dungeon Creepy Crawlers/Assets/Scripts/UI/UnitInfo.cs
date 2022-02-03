//Joshara Edwards (2022)

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitInfo : MonoBehaviour, InputSelectReceiver
{
    public GameObject unitInfoPrototype;

    private StatsTracker unitStats;
    private SelectionHandler selectionHandler;
    private GameObject unitInfoInst;
    private UIManager uiManager;

    void Start()
    {
        selectionHandler = GetComponent<SelectionHandler>();
        selectionHandler.Subscribe(this);
        unitStats = GetComponent<StatsTracker>();
        uiManager = UIManager.GetInstance();
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

        if (unitInfoInst != null)
            Destroy(unitInfoInst.gameObject);

        return null;
    }

    public SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        return null;
    }

    private void InitUnitInfoDisplay()
    {
        if(unitStats && unitInfoPrototype)
        {
            unitInfoPrototype.GetComponent<UnitInfoDisplay>().unitStats = unitStats;
            unitInfoInst = Instantiate(unitInfoPrototype, uiManager.GetUnitSpawnLocation());
        }
        else
        {
            Debug.Log("ERROR: Missing Unit Stats or UI prototype");
        }       
    }
}
