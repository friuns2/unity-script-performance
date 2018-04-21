using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

/// <summary>
/// Top-level test manager. 
/// This MonoBehaviour will run any ITestController 
/// tests on the child objects. To enable/disable a
/// test just turn the child object on/off. 
/// </summary>
public class TestController : MonoBehaviour
{
    [Header("This object will run tests on all active child test objects")]
    public int numIterations = 10;

    private ITestController[] tests;
    private int currentIteration;

    private void Awake()
    {
        UnityEngine.Profiling.Profiler.enabled = true;
    }

    void Start()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Init tests");
        currentIteration = 0;
        tests = GetComponentsInChildren<ITestController>();

        for ( int iTest = 0, iTestLen = tests.Length;
            iTest < iTestLen;
            iTest++)
        {
            if (tests[iTest] != null)
            {
                tests[iTest].Init();
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }

    void Update()
    {
        if (currentIteration < numIterations)
        {
            for (int iTest = 0, iTestLen = tests.Length;
                 iTest < iTestLen;
                 iTest++)
            {
                tests[iTest].Test();
            }
            currentIteration++;

            // Done, stop running
            if (currentIteration == numIterations)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
            else
            {
                // Try to flush memory for the next run
                System.GC.Collect();
            }
        }
    }
}
