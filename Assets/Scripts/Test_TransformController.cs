using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Project created by Garret Polk from Longsword Studios, Inc.
/// http://www.longswordstudios.com
/// Code comments are mine.
/// 
/// Code was taken from the slides of Søren Trautner Madsen
/// from the talk/video below. soren@playdead.com
///
/// https://docs.google.com/presentation/d/1dew0TynVmtQf8OMLEz_YtRxK32a_0SAZU9-vgyMRPlA
/// https://www.youtube.com/watch?v=mQ2KTRn4BMI&t
///
/// Performance improvements using a GameObject transform
/// </summary>
public class Test_TransformController : MonoBehaviour, ITestController
{
    public Test_Transform objTest;
    private int numIterations = 1000;

    public void Init()
    {
        if (objTest == null)
            Debug.LogError("No test object");
    }

    public void Test()
    {
        Test_Transform.globalDeltaTime = Time.deltaTime;

        // do a lot of iterations:
        UnityEngine.Profiling.Profiler.BeginSample("Transform");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : Reduce vector ops");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_ReduceVectorOps();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : Cached transforms");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_CachedTransforms();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : Local position");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_LocalPosition();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : Reduce engine calls");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_ReduceEngineCalls();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : No vector math");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_NoVectorMath();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : Cache delta time (get/set)");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_CacheDeltaTimeGetSet();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Transform : Cache delta time");
        for (int a = 0; a < numIterations; a++)
        {
            objTest.UpdateCharacter_CacheDeltaTime();
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}

