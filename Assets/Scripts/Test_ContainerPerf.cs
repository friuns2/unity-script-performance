using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LongswordStudios;

/// <summary>
/// Examples of performance improvements with containers.
/// Using containers with keys of type Enum.
/// </summary>
public class Test_ContainerPerf : MonoBehaviour
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

    void Start()
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

    void Update()
    {
        // Collections

        // Dictionary of ints
        int iTestInt = TestDictionaryInt();

        // Dictionary of strings
        // No GC allocs! Wow! ~1.21ms
        int iTestString = TestDictionaryString();

        // Allocs 117.2KB in ~2.03ms  
        int iTestEnum = TestDictionaryEnum();
        if (iTestEnum != iTestString)
            Debug.LogError("Mismatch in Enum");

        // No GC allocs in ~1.73ms
        int iTestEnumNoBox = TestDictionaryEnumNoBoxing();
        if (iTestEnumNoBox != iTestString)
            Debug.LogError("Mismatch in EnumNoBox");

        // Identical performance as EnumNoBoxing
        // No GC allocs in ~1.73ms
        int iTestEnumInt = TestDictionaryEnumInt();
        if (iTestEnumInt != iTestString)
            Debug.LogError("Mismatch in EnumNoBox");
    }

    int TestDictionaryEnum()
    {
        int number = 0;

        for (int iLoop = 0; iLoop < 1000; iLoop++)
        {
            if (dictEnumInt.ContainsKey(TEST_KEY.thing2))
            {
                number += dictEnumInt[TEST_KEY.thing2];
            }
        }
        return number;
    }

    int TestDictionaryEnumNoBoxing()
    {
        int number = 0;

        for (int iLoop = 0; iLoop < 1000; iLoop++)
        {
            if (dictEnumIntNoBox.ContainsKey(TEST_KEY.thing2))
            {
                number += dictEnumIntNoBox[TEST_KEY.thing2];
            }
        }
        return number;
    }

    int TestDictionaryEnumInt()
    {
        int number = 0;

        for (int iLoop = 0; iLoop < 1000; iLoop++)
        {
            if (dictEnumInt2Int.ContainsKey(TEST_KEY_INT.thing2))
            {
                number += dictEnumInt2Int[TEST_KEY_INT.thing2];
            }
        }
        return number;
    }

    int TestDictionaryString()
    {
        int number = 0;

        for (int iLoop = 0; iLoop < 1000; iLoop++)
        {
            if (dictStrInt.ContainsKey("12"))
            {
                number += dictStrInt["12"];
            }
        }
        return number;
    }

    int TestDictionaryInt()
    {
        int number = 0;

        for (int iLoop = 0; iLoop < 1000; iLoop++)
        {
            if (dictIntInt.ContainsKey(12))
            {
                number += dictIntInt[12];
            }
        }
        return number;
    }
}