using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GP.Utils;

/// <summary>
/// Test various C# collections for
/// speed of adding values. 
/// (Int32 and GameObjects)
/// </summary>
public class Test_Collection_Add_Int32 : MonoBehaviour, ITestController
{
    public int numIterations = 1000;
    GameObject[] goTests;

    public void Init()
    {
        goTests = new GameObject[numIterations];
        for (int i = 0; i < numIterations; i++)
        {
            goTests[i] = new GameObject();
        }
    }
    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : [], resize");
        {
            int[] arrayInt = new int[numIterations];
            //System.Array.Resize(ref arrayInt, numIterations);
            for (int i = 0; i < numIterations; i++)
            {
                arrayInt[i] = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Array Class, resize");
        {
            System.Array arrayClass = System.Array.CreateInstance(typeof(int), numIterations);
            //System.Array.Resize(ref arrayInt, numIterations);
            for (int i = 0; i < numIterations; i++)
            {
                arrayClass.SetValue(i, i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // LISTS
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : List, resize");
        {
            List<int> listInt = new List<int>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                listInt.Add(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : List");
        {
            List<int> listInt = new List<int>();

            for (int i = 0; i < numIterations; i++)
            {
                listInt.Add(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // ARRAYLIST
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : ArrayList, resize");
        {
            ArrayList arrayList = new ArrayList(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                arrayList.Add(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : ArrayList");
        {
            ArrayList listInt = new ArrayList();

            for (int i = 0; i < numIterations; i++)
            {
                listInt.Add(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // HASHSET
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : HashSet");
        {
            HashSet<int> hashset = new HashSet<int>();

            for (int i = 0; i < numIterations; i++)
            {
                hashset.Add(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Dictionary");
        {
            Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < numIterations; i++)
            {
                dict[i] = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Dictionary, resize");
        {
            Dictionary<int, int> dict = new Dictionary<int, int>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                dict[i] = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Linked List");
        {
            LinkedList<int> linkedList = new LinkedList<int>();

            for (int i = 0; i < numIterations; i++)
            {
                linkedList.AddLast(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Stack");
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < numIterations; i++)
            {
                stack.Push(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Stack, resize");
        {
            Stack<int> stack = new Stack<int>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                stack.Push(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();


        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Queue");
        {
            Queue<int> q = new Queue<int>();

            for (int i = 0; i < numIterations; i++)
            {
                q.Enqueue(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Queue, resize");
        {
            Queue<int> q = new Queue<int>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                q.Enqueue(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        //NEW
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : FastList, resize");
        FastList<int> abListResized = new FastList<int>(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            abListResized.Add(i);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : FastList");
        FastList<int> abList = new FastList<int>();
        for (int i = 0; i < numIterations; i++)
        {
            abList.Add(i);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Specialized list
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : FastListInt, resize");
        FastListInt fastListIntResized = new FastListInt(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            fastListIntResized.Add(i);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : FastListInt");
        FastListInt fastList = new FastListInt();
        for (int i = 0; i < numIterations; i++)
        {
            fastList.Add(i);
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
