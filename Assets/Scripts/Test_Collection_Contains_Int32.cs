using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GP.Utils;

/// <summary>
/// Test various C# collections for
/// speed of checking Contains(). 
/// (Int32)
/// </summary>
public class Test_Collection_Contains_Int32 : MonoBehaviour, ITestController
{
    public static int numIterations = 1000;
    public static int numCheckVals = 100;
    int[] checkVals = new int[numIterations];
    int[] arrayInt = new int[numIterations];
    List<int> listInt = new List<int>(numIterations);
    HashSet<int> hashset = new HashSet<int>();
    Dictionary<int, int> dict = new Dictionary<int, int>(numIterations);
    LinkedList<int> linkedList = new LinkedList<int>();
    Stack<int> stack = new Stack<int>(numIterations);
    Queue<int> q = new Queue<int>(numIterations);
    FastListInt fastList = new FastListInt(numIterations);
    System.Array arrayClass = System.Array.CreateInstance(typeof(int), numIterations);

    public void Init()
    {
        for (int i = 0; i < numCheckVals; i++)
        {
            checkVals[i] = UnityEngine.Random.Range(0, numIterations);
        }
        for (int i = 0; i < numIterations; i++)
        {
            arrayInt[i] = i;
            listInt.Add(i);
            hashset.Add(i);
            dict[i] = i;
            linkedList.AddLast(i);
            stack.Push(i);
            q.Enqueue(i);
            fastList.Add(i);
            arrayClass.SetValue(i, i);
        }
    }

    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : []");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                for (int i = 0; i < numIterations; i++)
                {
                    if (arrayInt[i] == checkVals[cv])
                        break;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // GetValue() allocates memory!!??!
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Array Class, resize");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                for (int i = 0; i < numIterations; i++)
                {
                    if (arrayClass.GetValue(i).Equals (checkVals[cv]))
                        break;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : List");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                if (!listInt.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : HashSet");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                if (!hashset.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : Dictionary");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                if (!dict.ContainsKey(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : Linked List");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                if (!linkedList.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();


        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : Stack");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                if (!stack.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : Queue");
        {
            for (int cv = 0; cv < numCheckVals; cv++)
            {
                if (!q.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, Int32) : FastListInt");
        for (int cv = 0; cv < numCheckVals; cv++)
        {
            if (!fastList.Contains(checkVals[cv]))
            {
                Debug.LogError("Bug!");
                return;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}

