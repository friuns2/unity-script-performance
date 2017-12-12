using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

/// <summary>
/// Testing Unity events
/// See also : https://jacksondunstan.com/articles/3335
/// </summary>
public class Test_UnityEvents : MonoBehaviour
{
    // An event that sends an int
    // These allocate!
    [Serializable]
    public class TestIntEvent : UnityEvent<int> {}
    public TestIntEvent evtInt = new TestIntEvent();

    // An event with no parameters
    // This does not allocate
    public UnityEvent evt = new UnityEvent();

    // Just a fake value so the function isn't
    // optimized out by the compiler.
    int numReceived = 0;

    public void Init()
    {
        //evt.AddListener(ReceiveEvent);
        evtInt.AddListener(ReceiveEventInt);
    }

    public void SendEvent()
    {
        //evt.Invoke();
        evtInt.Invoke(1);
    }

    public void ReceiveEventInt(int value)
    {
        numReceived++;
    }

    public void ReceiveEvent()
    {
        numReceived++;
    }
}
