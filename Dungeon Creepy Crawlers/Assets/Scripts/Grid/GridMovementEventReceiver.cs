using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface GridMovementEventReceiver
{
    public void GridMovementEventCallBack(Vector3Int cellPos);
}
