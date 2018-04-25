// Revised BSD License text at bottom
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Test various C# collections for
/// speed of removing keys.
/// (Int32)
/// </summary>
public class Test_Collection_Remove_Key_Int32 : MonoBehaviour, ITestController
{
    public static int numIterations = 100;
    HashSet<int> hashset = new HashSet<int>();
    Dictionary<int, int> dict = new Dictionary<int, int>(numIterations);
    LinkedList<int> linkedList = new LinkedList<int>();

    public void Init()
    {
        for (int i = 0; i < numIterations; i++)
        {
            hashset.Add(i);
            dict[i] = i;
            linkedList.AddLast(i);
        }
    }

    // Remove all, one at a time
    public void Test()
    {
        // Reset
        Init();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, key) : HashSet");
        {
            for (int i = 0; i < numIterations; i++)
            {
                hashset.Remove(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Collection (remove, key) : Dictionary");
        {
            for (int i = 0; i < numIterations; i++)
            {
                dict.Remove(i);
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
