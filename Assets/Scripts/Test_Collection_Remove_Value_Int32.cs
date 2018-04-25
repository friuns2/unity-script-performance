// Revised BSD License text at bottom
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

