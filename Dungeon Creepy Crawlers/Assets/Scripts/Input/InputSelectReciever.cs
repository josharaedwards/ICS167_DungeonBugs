using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InputSelectReciever
{
    public InputSelectReciever CallBack(); // Return desired selected object

    public InputSelectReciever CallBack(Vector3Int cellPos);
}
