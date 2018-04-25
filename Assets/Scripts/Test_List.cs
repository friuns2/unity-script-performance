// Revised BSD License text at bottom
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Test various methods of looping a C# List<int>
/// </summary>
public class Test_List : MonoBehaviour, ITestController
{
    List<int> scriptList = new List<int>();
    private int numIterations = 10000;

    public void Init()
    {
        // Build the test data
        for (int i = 0; i < numIterations; ++i)
        {
            scriptList.Add(i);
        }
    }

    public void Test()
    {
        int iTest = 0;

        // Basic for loop with cached length
        UnityEngine.Profiling.Profiler.BeginSample("List : basic loop");
        for (int i = 0; i < numIterations; ++i)
        {
            iTest = scriptList[i];
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Basic loop accessing .Count
        UnityEngine.Profiling.Profiler.BeginSample("List : using .Count");
        for (int i = 0; i < scriptList.Count; i++)
        {
            iTest = scriptList[i];
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // foreach()
        UnityEngine.Profiling.Profiler.BeginSample("List : foreach()");
        foreach ( int i in scriptList)
        {
            iTest = i;
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
