// Revised BSD License text at bottom
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Some sample code was taken from the slides of Søren Trautner Madsen
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
