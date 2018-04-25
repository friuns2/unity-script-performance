// Revised BSD License text at bottom
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
