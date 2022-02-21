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
        InitUnitInfoDisplay();

        return selectionHandler;
    }

    public SelectionHandler CallBackDeselect()
    {
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
            unitInfoInst = Instantiate(unitInfoPrototype, uiManager.GetUnitSpawnLocation());
            unitInfoInst.GetComponent<UnitInfoDisplay>().Init(unitStats);
        }
        else
        {
            Debug.Log("ERROR: Missing Unit Stats or UI prototype");
        }       
    }
}
