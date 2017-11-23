using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Test various methods of looping a C# List<int>
/// </summary>
public class Test_List : MonoBehaviour, ITestController
{
    List<int> scriptList = new List<int>();
    private int numIterations = 10000;

    public void Init()
    {
        // Build the test data
        for (int i = 0; i < numIterations; ++i)
        {
            scriptList.Add(i);
        }
    }

    public void Test()
    {
        int iTest = 0;

        // Basic for loop with cached length
        UnityEngine.Profiling.Profiler.BeginSample("List : basic loop");
        for (int i = 0; i < numIterations; ++i)
        {
            iTest = scriptList[i];
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Basic loop accessing .Count
        UnityEngine.Profiling.Profiler.BeginSample("List : using .Count");
        for (int i = 0; i < scriptList.Count; i++)
        {
            iTest = scriptList[i];
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // foreach()
        UnityEngine.Profiling.Profiler.BeginSample("List : foreach()");
        foreach ( int i in scriptList)
        {
            iTest = i;
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
