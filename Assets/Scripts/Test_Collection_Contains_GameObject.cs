using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GP.Utils;

/// <summary>
/// Test various C# collections for
/// speed of checking Contains(). 
/// (GameObjects)
/// </summary>
public class Test_Collection_Contains_GameObject : MonoBehaviour, ITestController
{
    public static int numIterations = 100;

    GameObject[] checkVals = new GameObject[numIterations];
    GameObject[] array = new GameObject[numIterations];
    List<GameObject> list = new List<GameObject>(numIterations);
    HashSet<GameObject> hashset = new HashSet<GameObject>();
    Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>(numIterations);
    LinkedList<GameObject> linkedList = new LinkedList<GameObject>();
    Stack<GameObject> stack = new Stack<GameObject>(numIterations);
    Queue<GameObject> q = new Queue<GameObject>(numIterations);
    FastListGO fastList = new FastListGO(numIterations);

    public void Init()
    {
        for (int i = 0; i < numIterations; i++)
        {
            checkVals[i] = new GameObject();
        }
        for (int i = 0; i < numIterations; i++)
        {
            array[i] = checkVals[i];
            list.Add(checkVals[i]);
            hashset.Add(checkVals[i]);
            dict[checkVals[i]] = i;
            linkedList.AddLast(checkVals[i]);
            stack.Push(checkVals[i]);
            q.Enqueue(checkVals[i]);
            fastList.Add(checkVals[i]);
        }
    }

    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, GameObject) : Array");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                for (int i = 0; i < numIterations; i++)
                {
                    if (array[i] == checkVals[cv])
                        break;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : List");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                if (!list.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, GameObject) : List");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                for (int i = 0; i < numIterations; i++)
                {
                    if (list[i] == checkVals[cv])
                        break;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : HashSet");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                if (!hashset.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : Dictionary");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                if (!dict.ContainsKey(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : Linked List");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                if (!linkedList.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();


        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : Stack");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                if (!stack.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : Queue");
        {
            for (int cv = 0; cv < numIterations; cv++)
            {
                if (!q.Contains(checkVals[cv]))
                {
                    Debug.LogError("Bug!");
                    return;
                }
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (contains, GameObject) : FastListGO ");
        for (int cv = 0; cv < numIterations; cv++)
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

