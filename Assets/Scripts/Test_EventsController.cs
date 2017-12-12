using UnityEngine;
using System.Collections;

public class Test_EventsController : MonoBehaviour, ITestController
{
    public int numIterations = 1000;
    Test_UnityEvents unityEvents;
    Test_Action actionEvents;
    Test_SendMessage sendMsg;
    Test_UnityMessage unityMsg;

    public void Init()
    {
        unityEvents = GetComponentInChildren<Test_UnityEvents>();
        unityEvents.Init();

        actionEvents = GetComponentInChildren<Test_Action>();
        actionEvents.Init();

        sendMsg = GetComponentInChildren<Test_SendMessage>();
        sendMsg.Init();

        unityMsg = GetComponentInChildren<Test_UnityMessage>();
        unityMsg.Init();
    }

    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Unity events");
        for (int a = 0; a < numIterations; a++)
        {
            unityEvents.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Action events");
        for (int a = 0; a < numIterations; a++)
        {
            actionEvents.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Send message");
        for (int a = 0; a < numIterations; a++)
        {
            sendMsg.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Unity messaging");
        for (int a = 0; a < numIterations; a++)
        {
            unityMsg.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
