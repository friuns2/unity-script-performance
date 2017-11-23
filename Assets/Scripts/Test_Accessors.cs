using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LongswordStudios;

/// <summary>
/// Harness for accessor (get/set) tests.
/// </summary>
public class Test_Accessors : MonoBehaviour, ITestController
{
    int accessorIterations = 10000;
    int vector3Iterations = 1000;

    bool genericValueAccessors { get; set; }
    bool genericValue;

    public void Init()
    {
        genericValue = true;
        genericValueAccessors = true;
    }

    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Accessors functions (yes)");
        for (int iTest = 0; iTest < accessorIterations; iTest++)
            genericValueAccessors = !genericValueAccessors;
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Accessors functions (no)");
        for (int iTest = 0; iTest < accessorIterations; iTest++)
            genericValue = !genericValue;
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Vector3 (Unity)");
        for (int iTest = 0; iTest < vector3Iterations; iTest++)
        {
            Vector3 newVec = Vector3.zero;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Vector3 (static)");
        for (int iTest = 0; iTest < vector3Iterations; iTest++)
        {
            Vector3 newVec = Utils_Perf.vec3_zero;
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
