using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSelectManager : MonoBehaviour
{
    private InputSelectReciever selectedObject = null;

    private bool nextFrameCallBack = false;

    private HighlightGrid highlightGrid = null;
    private GridManager gridManager = null;

    void Start()
    {
        highlightGrid = HighlightGrid.Instance;
        gridManager = GridManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (nextFrameCallBack)
        {
            HandleCallBack();
            nextFrameCallBack = false;
        }

        if (Input.GetMouseButtonDown(0)) // delay handle input by 1 frame to ensure highlighted tile is correct
            nextFrameCallBack = true;

        if (Input.GetMouseButtonDown(1)) // Deselect
            selectedObject = null;
    }

    private void HandleCallBack()
    {
        Vector3Int selectedCell = highlightGrid.GetHighlightedCellPos();
        Debug.Log(selectedCell.ToString());
        if (selectedObject == null)
        {
            GameObject obj = gridManager.GetObjectFromCell(selectedCell);
            if (obj != null)
                selectedObject = obj.GetComponent<InputSelectReciever>();
            if (selectedObject != null)
            {
                selectedObject = selectedObject.CallBack();
            }

        }
        else
        {
            selectedObject = selectedObject.CallBack(selectedCell);
        }

    }
}
