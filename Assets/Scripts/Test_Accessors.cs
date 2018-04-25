// Revised BSD License text at bottom
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GP.Utils;

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
