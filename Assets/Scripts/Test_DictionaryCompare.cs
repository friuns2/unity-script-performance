// Revised BSD License text at bottom
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GP.Utils;

/// <summary>
/// Examples of performance improvements with containers.
/// Using containers with keys of type Enum.
/// </summary>
public class Test_DictionaryCompare : MonoBehaviour, ITestController
{
    public enum TEST_KEY
    {
        thing1,
        thing2,
        thing3
    }

    public enum TEST_KEY_INT
    {
        thing1 = 1,
        thing2 = 2,
        thing3 = 3,
    }

    // Use these to avoid GC memory allocations when
    // using Enums as a key
    Utils_Perf.EnumIntEqComp<TEST_KEY> noboxCompare;
    Utils_Perf.EnumIntEqComp<TEST_KEY_INT> noboxCompareInt;

    // Key : string
    Dictionary<string, int> dictStrInt = new Dictionary<string, int>();

    // Key : int
    Dictionary<int, int> dictIntInt = new Dictionary<int, int>();

    // Key : enum
    Dictionary<TEST_KEY, int> dictEnumInt = new Dictionary<TEST_KEY, int>();

    // Key : enum
    Dictionary<TEST_KEY, int> dictEnumIntNoBox;

    // Key : enum of integers
    Dictionary<TEST_KEY_INT, int> dictEnumInt2Int = new Dictionary<TEST_KEY_INT, int>();

    int numIterations = 1000;

    public void Init()
    {
        // Dictionaries
        noboxCompare = new Utils_Perf.EnumIntEqComp<TEST_KEY>();
        dictEnumIntNoBox = new Dictionary<TEST_KEY, int>(noboxCompare);
        dictEnumIntNoBox.Add(TEST_KEY.thing2, 12);

        dictEnumInt2Int = new Dictionary<TEST_KEY_INT, int>(noboxCompareInt);
        dictEnumInt2Int.Add(TEST_KEY_INT.thing2, 12);

        dictEnumInt.Add(TEST_KEY.thing2, 12);
        dictIntInt.Add(12, 12);
        dictStrInt.Add("12", 12);
    }

    public void Test()
    {
        // Collections

        int number = 0;
        // Dictionary of ints
        UnityEngine.Profiling.Profiler.BeginSample("Dictionary (iterate and add) : int");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (dictIntInt.ContainsKey(12))
            {
                number += dictIntInt[12];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Dictionary of strings
        // No GC allocs! Wow! ~1.21ms
        number = 0;
        UnityEngine.Profiling.Profiler.BeginSample("Dictionary (iterate and add) : string");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (dictStrInt.ContainsKey("12"))
            {
                number += dictStrInt["12"];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Allocs 117.2KB in ~2.03ms  
        number = 0;
        UnityEngine.Profiling.Profiler.BeginSample("Dictionary (iterate and add) : enum, boxing");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (dictEnumInt.ContainsKey(TEST_KEY.thing2))
            {
                number += dictEnumInt[TEST_KEY.thing2];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // No GC allocs in ~1.73ms
        number = 0;
        UnityEngine.Profiling.Profiler.BeginSample("Dictionary (iterate and add) : enum, no-boxing");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (dictEnumIntNoBox.ContainsKey(TEST_KEY.thing2))
            {
                number += dictEnumIntNoBox[TEST_KEY.thing2];
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Identical performance as EnumNoBoxing
        // No GC allocs in ~1.73ms
        number = 0;
        UnityEngine.Profiling.Profiler.BeginSample("Dictionary (iterate and add) : enum of int");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (dictEnumInt2Int.ContainsKey(TEST_KEY_INT.thing2))
            {
                number += dictEnumInt2Int[TEST_KEY_INT.thing2];
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
