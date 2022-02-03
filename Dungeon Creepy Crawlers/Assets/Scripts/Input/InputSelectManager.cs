using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputSelectManager : MonoBehaviour, TurnEventReciever
{
    private SelectionHandler selectedObject = null;
    private TurnEventHandler turnEventHandler;

    private bool nextFrameCallBack = false;

    private HighlightGrid highlightGrid;
    private GridManager gridManager;


    void Start()
    {
        highlightGrid = HighlightGrid.GetInstance();
        gridManager = GridManager.GetInstance();

        turnEventHandler = GetComponent<TurnEventHandler>();
        turnEventHandler.Subscribe(this);

    }

    // Update is called once per frame
    void Update()
    {
        if (nextFrameCallBack)
        {
            HandleCallBackSelect();
            nextFrameCallBack = false;
        }

        // Select with left-click
        if (Input.GetMouseButtonDown(0)) 
            nextFrameCallBack = true; // delay handle input by 1 frame to ensure highlighted tile is correct

        if (Input.GetMouseButtonDown(1)) // Deselect with right-click
            HandleCallBackDeselect();
    }

    private void HandleCallBackSelect()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        SelectionHandler t; // Temp variable to hold value;
        Vector3Int selectedCell = highlightGrid.GetHighlightedCellPos();
        Debug.Log(selectedCell.ToString());

        if (selectedObject == null)
        {
            GameObject obj = gridManager.GetObjectFromCell(selectedCell);
            if (obj != null)
                selectedObject = obj.GetComponent<SelectionHandler>();
            if (selectedObject != null)
            {
                t = selectedObject.CallBackSelect();
                if (t == null) // Call deselect if returns null
                {
                    HandleCallBackDeselect();
                }
                selectedObject = t;
            }
        }
        else
        {
            t = selectedObject.CallBackSelect(selectedCell);
            if (t == null) // Call deselect if returns null
            {
                HandleCallBackDeselect();
            }
            selectedObject = t;
            if (selectedObject == null) // If selectedobj return null then try to find what SelectionHandler in selectedCell and call that
            {
                GameObject g;
                g = gridManager.GetObjectFromCell(selectedCell);
                if (g != null)
                {
                    selectedObject = g.GetComponent<SelectionHandler>();
                    selectedObject = selectedObject.CallBackSelect();
                }
            }
        }
    }

    private void HandleCallBackDeselect()
    {
        if (selectedObject != null)
        {
            selectedObject = selectedObject.CallBackDeselect();
            if (selectedObject != null) // Call select again if deselect return something;
            {
                selectedObject = selectedObject.CallBackSelect();
            }
        }
        
    }

    public void CallBackTurnEvent(GameManager.TurnState turnState)
    {
        HandleCallBackDeselect();
        selectedObject = null; // Deselect regardless at turn change
        Debug.Log("DESELECTED EVERYTHING");
    }
}
