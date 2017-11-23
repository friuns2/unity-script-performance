using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LongswordStudios;

/// <summary>
///  Tests the speeds of various methods of String
/// </summary>
public class Test_StringPerf : MonoBehaviour, ITestController
{
    string s1Test1 = "1Test1";
    string sTest = "Test";
    string sTest2Test = "Test2Test";
    int numIterations = 1000;

    public void Init()
    {
    }

    public void Test()
    {
        int matches = 0;
        string sTestTemp = "Test";

        UnityEngine.Profiling.Profiler.BeginSample("String (compare) : Equals");
        for (int iTest = 0; iTest < numIterations; iTest++)
        {
            // Checks to see if the contents of a string
            // matches exactly.
            if (sTest.Equals(sTestTemp))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (compare) : ==");

        for (int iTest = 0; iTest < numIterations; iTest++)
        {
            // Checks to see if the contents of a string
            // matches exactly.
            if (sTest == sTestTemp)
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (compare) : CompareTo");
        for (int iTest = 0; iTest < numIterations; iTest++)
        { 
            // Used primarily for sorting, esp. on
            // localized strings. C# can do the localized
            // sort for you! But...
            //
            // 0 == CompareTo() is 70x slower than Equals()
            if (0 == sTest.CompareTo(sTestTemp))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        // CONTAINS
        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (contains) : Contains");
        for (int iTest = 0; iTest < numIterations; iTest++)
        {
            // String.Contains() calls String.IndexOf()
            // which calls CultureInfo stuff, which is slow.
            if (s1Test1.Contains(sTest))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (contains) : byte-wise contains");
        for (int iTest = 0; iTest < numIterations; iTest++)
        {
            if (Utils_Perf.Contains(s1Test1, sTest))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        // Ends With
        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (EndsWith) : EndsWith");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (sTest2Test.EndsWith(sTest))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (EndsWith) : byte-wise EndsWith");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (Utils_Perf.EndsWith(sTest2Test, sTest))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        // Starts with
        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (StartsWith) : StartsWith");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (sTest2Test.StartsWith(sTest))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");

        matches = 0;
        UnityEngine.Profiling.Profiler.BeginSample("String (StartsWith) : byte-wise StartsWith");
        for (int iLoop = 0; iLoop < numIterations; iLoop++)
        {
            if (Utils_Perf.StartsWith(sTest2Test, sTest))
                matches++;
        }
        UnityEngine.Profiling.Profiler.EndSample();
        if (matches != numIterations)
            Debug.LogError("Mismatch");
    }
}