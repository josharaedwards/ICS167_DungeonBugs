using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour, InputSelectReciever
{
    private List<InputSelectReciever> inputSelectRecievers;


    void Awake()
    {
        inputSelectRecievers = new List<InputSelectReciever>();
    }

    public SelectionHandler CallBackSelect()  // Callback when selected
    {
        foreach (InputSelectReciever reciever in inputSelectRecievers)
        {
            reciever.CallBackSelect();
        }
        return this;
        
    }
    public SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        SelectionHandler r = null;
        foreach (InputSelectReciever reciever in inputSelectRecievers)
        {
            SelectionHandler t = reciever.CallBackSelect(cellPos);
            if (r == null)                                          // Will only return the first non-null CallBackSelect return
                r = t;
        }
        return r;
    }

    public SelectionHandler CallBackDeselect() // Callback when deslected
    {
        foreach (InputSelectReciever reciever in inputSelectRecievers)
        {
            reciever.CallBackDeselect();
        }
        return null;
    }

    public void Subscribe(InputSelectReciever reciever)
    {
        inputSelectRecievers.Add(reciever);
    }
}
