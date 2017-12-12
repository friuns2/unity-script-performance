using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface IMsgTest : IEventSystemHandler
{
    void ReceiveEvent(int value);
}

public class Test_UnityMessage : 
    MonoBehaviour,
    IMsgTest
{
    int numReceived = 0;

    public void Init()
    {
        
    }

    public void SendEvent()
    {
        ExecuteEvents.Execute<IMsgTest>(gameObject, null, (x,y) => x.ReceiveEvent(1));
    }

    public void ReceiveEvent(int value)
    {
        numReceived++;
    }
}
