using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputSelectReciever
{
    // Return desired selected object
    public SelectionHandler CallBackSelect();  // Callback when selected
    public SelectionHandler CallBackSelect(Vector3Int cellPos);

    public SelectionHandler CallBackDeselect(); // Callback when deslected

}
