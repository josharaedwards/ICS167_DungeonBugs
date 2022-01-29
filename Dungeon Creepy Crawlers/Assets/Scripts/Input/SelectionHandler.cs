using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionHandler : MonoBehaviour, InputSelectReceiver
{
    private List<InputSelectReceiver> inputSelectReceivers;


    void Awake()
    {
        inputSelectReceivers = new List<InputSelectReceiver>();
    }

    public SelectionHandler CallBackSelect()  // Callback when selected
    {
        foreach (InputSelectReceiver reciever in inputSelectReceivers)
        {
            reciever.CallBackSelect();
        }
        return this;
        
    }
    public SelectionHandler CallBackSelect(Vector3Int cellPos)
    {
        SelectionHandler r = null;
        foreach (InputSelectReceiver reciever in inputSelectReceivers)
        {
            SelectionHandler t = reciever.CallBackSelect(cellPos);
            if (r == null)                                          // Will only return the first non-null CallBackSelect return
                r = t;
        }
        return r;
    }

    public SelectionHandler CallBackDeselect() // Callback when deslected
    {
        foreach (InputSelectReceiver reciever in inputSelectReceivers)
        {
            reciever.CallBackDeselect();
        }
        return null;
    }

    public void Subscribe(InputSelectReceiver reciever)
    {
        inputSelectReceivers.Add(reciever);
    }
}
