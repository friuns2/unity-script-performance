using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LongswordStudios;

public class Test_StringPerf : MonoBehaviour
{
    string s1Test1 = "1Test1";
    string sTest = "Test";
    string sTest2Test = "Test2Test";

    void Update()
    {
        TestStartsWith();
        TestEndsWith();
        TestStringContains();
        TestCompares();
    }

    void TestCompares()
    {
        int matches1 = 0;
        int matches2 = 0;
        string sTestTemp = "Test";

        for (int iTest = 0; iTest < 1000; iTest++)
        {
            // Checks to see if the contents of a string
            // matches exactly.
            if (sTest.Equals(sTestTemp))
                matches1++;

            // Used primarily for sorting, esp. on
            // localized strings. C# can do the localized
            // sort for you! But...
            //
            // CompareTo() is 70x slower than Equals()
            if (0 == sTest.CompareTo(sTestTemp))
                matches2++;
        }

        if (matches1 != matches2)
            Debug.LogError("Mismatch");
    }

    void TestStringContains()
    {
        //string s1 = "1test1";
        //string s2 = "test";
        int matches1 = 0;
        int matches2 = 0;

        for (int iTest = 0; iTest < 1000; iTest++)
        {
            // String.Contains() calls String.IndexOf()
            // which calls CultureInfo stuff, which is slow.
            if (s1Test1.Contains(sTest))
                matches1++;

            if (Utils_Perf.Contains(s1Test1, sTest))
                matches2++;
        }

        if (matches1 != matches2)
            Debug.LogError("Mismatch");
    }

    void TestEndsWith()
    {
        //string s1 = "test2test";
        //string s2 = "test";
        int matches1 = 0;
        int matches2 = 0;

        for (int iLoop = 0; iLoop < 100; iLoop++)
        {
            if (sTest2Test.EndsWith(sTest))
                matches1++;
        }

        for (int iLoop = 0; iLoop < 100; iLoop++)
        {
            if (Utils_Perf.EndsWith(sTest2Test, sTest))
                matches2++;
        }

        if (matches1 != matches2)
            Debug.LogError("Mismatch");
    }

    void TestStartsWith()
    {
        //string s1 = "test2test";
        //string s2 = "test";
        int matches1 = 0;
        int matches2 = 0;

        for (int iLoop = 0; iLoop < 100; iLoop++)
        {
            if (sTest2Test.StartsWith(sTest))
                matches1++;
        }

        for (int iLoop = 0; iLoop < 100; iLoop++)
        {
            if (Utils_Perf.StartsWith(sTest2Test, sTest))
                matches2++;
        }

        if (matches1 != matches2)
            Debug.LogError("Mismatch");
    }
}