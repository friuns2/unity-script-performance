// Revised BSD License text at bottom
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using GP.Utils;

/// <summary>
/// Test various C# collections for
/// speed of iterating over values. 
/// (Int32)
/// </summary>
public class Test_Collection_Iteration_Int32 : MonoBehaviour, ITestController
{
    public int numItems = 100000;
    int[] arrayInt;
    List<int> listInt;
    HashSet<int> hashset;
    Dictionary<int, int> dict;
    LinkedList<int> linkedList;
    Stack<int> stack;
    Queue<int> q;
    FastListInt fastList;
    System.Array arrayClass;

    public void Init()
    {
        arrayInt = new int[numItems];
        listInt = new List<int>(numItems);
        hashset = new HashSet<int>();
        dict = new Dictionary<int, int>(numItems);
        linkedList = new LinkedList<int>();
        stack = new Stack<int>(numItems);
        q = new Queue<int>(numItems);
        fastList = new FastListInt(numItems);
        arrayClass = System.Array.CreateInstance(typeof(int), numItems);

        for (int i = 0; i < numItems; i++)
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
        int item = 0;
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : array [] : " + numItems);
        {
            for (int i = 0; i < numItems; ++i)
            {
                item = arrayInt[i];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : List[] : " + numItems);
        {
            for (int i = 0; i < numItems; ++i)
            {
                item = listInt[i];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : List foreach : " + numItems);
        {
            foreach (int i in listInt)
            {
                item = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Hashset foreach : " + numItems);
        {
            foreach (int i in hashset)
            {
                item = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Dictionary foreach : " + numItems);
        {
            foreach (KeyValuePair<int, int> pair in dict)
            {
                item = pair.Value;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Linked List foreach : " + numItems);
        {
            foreach (int i in linkedList)
            {
                item = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Stack foreach : " + numItems);
        {
            foreach (int i in stack)
            {
                item = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Queue foreach : " + numItems);
        {
            foreach (int i in q)
            {
                item = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : FastListInt [] : " + numItems);
        {
            for (int i = 0; i < numItems; ++i)
            {
                item = fastList[i];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : FastListInt for/GetAt : " + numItems);
        {
            for (int i = 0; i < numItems; ++i)
            {
                item = fastList.GetAt(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : FastListInt for/RawArray : " + numItems);
        {
            int[] entries = fastList.RawArray();
            for (int i = 0; i < numItems; ++i)
            {
                item = entries[i];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : FastListInt foreach : " + numItems);
        {
            foreach (int i in fastList)
            {
                item = i;
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Array class [] : " + numItems);
        {
            for (int i = 0; i < numItems; ++i)
            {
                item = (int)arrayClass.GetValue(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
        UnityEngine.Profiling.Profiler.BeginSample("Collection (iterate, Int32) : Array class foreach : " + numItems);
        {
            foreach (int i in arrayClass)
            {
                item = i;
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
