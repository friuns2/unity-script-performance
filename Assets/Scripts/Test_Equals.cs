using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GP.Utils;

/// <summary>
/// Test speed of == vs. .Equals for various types
/// </summary>
public class Test_Equals : MonoBehaviour, ITestController
{
    public static int numIterations = 10000;

    public void Init()
    {
    }

    public void Test()
    {
        GameObject go1 = new GameObject();
        GameObject go2 = new GameObject();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (go1 == go2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (go1.Equals(go2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : !go1");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (!go1)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : == null");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (go1 == null)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        int i1 = 1;
        int i2 = 2;

        UnityEngine.Profiling.Profiler.BeginSample("Equals (int) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i1 == i2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (int) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i1.Equals(i2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // String
        string s1 = "1";
        string s2 = "2";

        UnityEngine.Profiling.Profiler.BeginSample("Equals (string) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (s1.Equals(s2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (string) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (s1 == s2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // bool
        bool b1 = true;
        bool b2 = false;

        UnityEngine.Profiling.Profiler.BeginSample("Equals (bool) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (b1.Equals(b2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (bool) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (b1 == b2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}

