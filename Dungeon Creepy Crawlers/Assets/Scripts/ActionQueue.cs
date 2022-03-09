using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionQueue : MonoBehaviour
{
    public delegate IEnumerator actionCoroutine();
    private List<actionCoroutine> actionQueue;

    void Awake()
    {
        actionQueue = new List<actionCoroutine>();
    }

    public void Add(actionCoroutine action)
    {
        actionQueue.Add(action);
    }

    public IEnumerator Execute()
    {
        foreach (actionCoroutine action in actionQueue)
        {
            yield return StartCoroutine(action());
        }
        actionQueue.Clear();
        yield return null;
    }

}
