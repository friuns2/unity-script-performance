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
public class Test_Collection_Add_GameObject : MonoBehaviour, ITestController
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
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Array, resize");
        {
            GameObject[] arrayGO = new GameObject[numIterations];
            //System.Array.Resize(ref arrayGO, numIterations);
            for (int i = 0; i < numIterations; i++)
            {
                arrayGO[i] = goTests[i];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // LISTS
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : List, resize");
        {
            List<GameObject> listGO = new List<GameObject>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                listGO.Add(goTests[i]);
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
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : ArrayList, resize");
        {
            ArrayList listGO = new ArrayList(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                listGO.Add(goTests[i]);
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

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : Dictionary, resize");
        {
            Dictionary<GameObject, int> dict = new Dictionary<GameObject, int>(numIterations);

            for (int i = 0; i < numIterations; i++)
            {
                dict[goTests[i]] = i;
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

        //NEW

        // Generic FastList
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : FastList<T>, resize");
        FastList<GameObject> abListGOResized = new FastList<GameObject>(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            abListGOResized.Add(goTests[i]);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : FastList<T>");
        FastList<GameObject> abListGO = new FastList<GameObject>();
        for (int i = 0; i < numIterations; i++)
        {
            abListGO.Add(goTests[i]);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Specialized FastList
        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : FastListGO, resize");
        FastListGO fastListGOResized = new FastListGO(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            fastListGOResized.Add(goTests[i]);
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (add, GameObject) : FastListGO");
        FastListGO fastListGO = new FastListGO();
        for (int i = 0; i < numIterations; i++)
        {
            fastListGO.Add(goTests[i]);
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
