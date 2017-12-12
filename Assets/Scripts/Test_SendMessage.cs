using UnityEngine;
using System.Collections;

public class Test_SendMessage : MonoBehaviour
{
    int numReceived = 0;

    public void Init()
    {

    }

    public void SendEvent()
    {
        gameObject.SendMessage("ReceiveEvent", 1, SendMessageOptions.RequireReceiver);
    }

    public void ReceiveEvent (int value)
    {
        numReceived++;
    }
}
