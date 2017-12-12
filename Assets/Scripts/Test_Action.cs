using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Testing standard C# Action as an event system.
/// </summary>
public class Test_Action : MonoBehaviour
{
    public Action<int> actionChanged;
    private int numReceived = 0;

    public void Init()
    {
        actionChanged += ReceiveEvent;
    }

    public void SendEvent()
    {
        actionChanged.Invoke(1);
    }

    public void ReceiveEvent(int value)
    {
        numReceived++;
    }
}
