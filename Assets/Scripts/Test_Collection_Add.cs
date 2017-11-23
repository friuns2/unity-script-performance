using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using LongswordStudios;

/// <summary>
/// Test various C# collections for
/// speed of adding values. 
/// (Int32 and GameObjects)
/// </summary>
public class Test_Collection_Add : MonoBehaviour, ITestController
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
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : Array");
        {
            int[] arrayInt = new int[1];
            System.Array.Resize(ref arrayInt, numIterations);
            for (int i = 0; i < numIterations; i++)
            {
                arrayInt[i] = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Array");
        {
            GameObject[] arrayGO = new GameObject[1];
            System.Array.Resize(ref arrayGO, numIterations);
            for (int i = 0; i < numIterations; i++)
            {
                arrayGO[i] = goTests[i];
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : List, resize");
        {
            List<GameObject> listGO = new List<GameObject>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                listGO.Add(goTests[i]);
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : List");
        {
            List<GameObject> listGO = new List<GameObject>();

            for (int i = 0; i < numIterations; i++)
            {
                listGO.Add(goTests[i]);
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : ArrayList, resize");
        {
            ArrayList listGO = new ArrayList(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                listGO.Add(goTests[i]);
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : ArrayList");
        {
            ArrayList listGO = new ArrayList();

            for (int i = 0; i < numIterations; i++)
            {
                listGO.Add(goTests[i]);
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : HashSet");
        {
            HashSet<GameObject> hashset = new HashSet<GameObject>();

            for (int i = 0; i < numIterations; i++)
            {
                hashset.Add(goTests[i]);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Dictionary");
        {
            Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>();

            for (int i = 0; i < numIterations; i++)
            {
                dict[goTests[i]] = i;
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Dictionary, resize");
        {
            Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                dict[goTests[i]] = i;
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Stack");
        {
            Stack<GameObject> stack = new Stack<GameObject>();

            for (int i = 0; i < numIterations; i++)
            {
                stack.Push(goTests[i]);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Stack, resize");
        {
            Stack<GameObject> stack = new Stack<GameObject>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                stack.Push(goTests[i]);
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Queue");
        {
            Queue<GameObject> q = new Queue<GameObject>();

            for (int i = 0; i < numIterations; i++)
            {
                q.Enqueue(goTests[i]);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Queue, resize");
        {
            Queue<GameObject> q = new Queue<GameObject>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                q.Enqueue(goTests[i]);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, Int32) : FastList<T>, resize");
        Utils_FastList<int> fastList = new Utils_FastList<int>(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            fastList.Add(i);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : FastList<T>, resize");
        Utils_FastList<GameObject> fastListGO = new Utils_FastList<GameObject>(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            fastListGO.Add(goTests[i]);
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
