using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectTest : MonoBehaviour, InputSelectReciever
{

    private GridManager gridManager = null;
    private Vector3Int currentCellPos;

    // Start is called before the first frame update
    void Start()
    {
        gridManager = GridManager.Instance;
        currentCellPos = gridManager.AddObject(gameObject, Vector3.zero);
        transform.position = gridManager.cellToWorld(currentCellPos);
        transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public InputSelectReciever CallBack()
    {
        return this;
    }

    public InputSelectReciever CallBack(Vector3Int cellPos)
    {
        bool t = gridManager.MoveObject(gameObject, cellPos);
        if (t)
        {
            currentCellPos = cellPos;
            transform.position = gridManager.cellToWorld(currentCellPos);
            transform.position = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.5f, transform.position.z);

            return this;
        }

        return null;
        
    }
}
