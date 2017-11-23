using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LongswordStudios;

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