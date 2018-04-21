using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GP.Utils;

/// <summary>
/// Test various C# collections for
/// speed of removing values.
/// (Int32)
/// </summary>
public class Test_Collection_Remove_Value_Int32 : MonoBehaviour, ITestController
{
    public static int numIterations = 1000;
    int[] arrayInt = new int[numIterations];
    List<int> listInt = new List<int>(numIterations);
    HashSet<int> hashset = new HashSet<int>();
    Dictionary<int, int> dict = new Dictionary<int, int>(numIterations);
    LinkedList<int> linkedList = new LinkedList<int>();
    Stack<int> stack = new Stack<int>(numIterations);
    Queue<int> q = new Queue<int>(numIterations);
    GP.Utils.FastListInt fastList;

    public void Init()
    {
        if (fastList == null)
            fastList = new GP.Utils.FastListInt(numIterations);

        fastList.Clear();
        listInt.Clear();
        hashset.Clear();
        dict.Clear();
        linkedList.Clear();
        stack.Clear();
        q.Clear();

        for (int i = 0; i < numIterations; i++)
        {
            arrayInt[i] = i;
            listInt.Add(i);
            hashset.Add(i);
            dict[i] = i;
            linkedList.AddLast(i);
            stack.Push(i);
            q.Enqueue(i);
        }
    }

    // Remove half the values
    public void Test()
    {
        // Reset
        Init();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : Array");
        {
            // Technically this isn't removing, just
            // setting the value. This won't effect
            // memory so it's not exactly comparable.
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    arrayInt[i] = 0;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : List");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    listInt.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : HashSet");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    hashset.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : Dictionary");
        {
            for ( int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    dict.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : Linked List");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    linkedList.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Stack - not really Remove, but Pop is
        // close.
        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : Stack");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    stack.Pop();
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Queue - not really Remove, but Dequeue is
        // close.
        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : Queue");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    q.Dequeue();
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, Int32) : FastListInt");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i % 2 == 0)
                    fastList.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}



