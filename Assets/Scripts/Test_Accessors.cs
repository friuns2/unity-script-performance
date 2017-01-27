using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LongswordStudios;

/// <summary>
/// Harness for accessor (get/set) tests.
/// </summary>
public class Test_Accessors : MonoBehaviour
{
    void Update()
    {
        TestRemovingAccessors();
    }

    void TestRemovingAccessors()
    {
        for (int iTest = 0; iTest < 1000; iTest++)
        {
            TestVector3Zero();
        }

        for (int iTest = 0; iTest < 1000; iTest++)
        {
            TestFastVector3Zero();
        }
    }

    void TestVector3Zero()
    {
        Vector3 newVec = Vector3.zero;
    }

    void TestFastVector3Zero()
    {
        Vector3 newVec = Utils_Perf.vec3_zero;
    }
}
