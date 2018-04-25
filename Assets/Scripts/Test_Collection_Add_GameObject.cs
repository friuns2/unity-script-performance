// Revised BSD License text at bottom
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
/*
Revised BSD License

Copyright(c) 2018, Garret Polk
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:
    * Redistributions of source code must retain the above copyright
      notice, this list of conditions and the following disclaimer.
    * Redistributions in binary form must reproduce the above copyright
      notice, this list of conditions and the following disclaimer in the
      documentation and/or other materials provided with the distribution.
    * Neither the name of the Garret Polk nor the
      names of its contributors may be used to endorse or promote products
      derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL GARRET POLK BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
*/
