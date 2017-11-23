using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Test various C# collections for
/// speed of removing keys.
/// (Int32)
/// </summary>
public class Test_Collection_Remove_Key_Int32 : MonoBehaviour, ITestController
{
    public static int numIterations = 100;
    HashSet<int> hashset = new HashSet<int>();
    Dictionary<int, int> dict = new Dictionary<int, int>(numIterations);
    LinkedList<int> linkedList = new LinkedList<int>();

    public void Init()
    {
        for (int i = 0; i < numIterations; i++)
        {
            hashset.Add(i);
            dict[i] = i;
            linkedList.AddLast(i);
        }
    }

    // Remove all, one at a time
    public void Test()
    {
        // Reset
        Init();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, key) : HashSet");
        {
            for (int i = 0; i < numIterations; i++)
            {
                hashset.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, key) : Dictionary");
        {
            for (int i = 0; i < numIterations; i++)
            {
                dict.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}


