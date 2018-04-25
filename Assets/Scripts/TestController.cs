// Revised BSD License text at bottom
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections;

/// <summary>
/// Top-level test manager. 
/// This MonoBehaviour will run any ITestController 
/// tests on the child objects. To enable/disable a
/// test just turn the child object on/off. 
/// </summary>
public class TestController : MonoBehaviour
{
    [Header("This object will run tests on all active child test objects")]
    public int numIterations = 10;

    private ITestController[] tests;
    private int currentIteration;

    private void Awake()
    {
        UnityEngine.Profiling.Profiler.enabled = true;
    }

    void Start()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Init tests");
        currentIteration = 0;
        tests = GetComponentsInChildren<ITestController>();

        for ( int iTest = 0, iTestLen = tests.Length;
            iTest < iTestLen;
            iTest++)
        {
            if (tests[iTest] != null)
            {
                tests[iTest].Init();
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }

    void Update()
    {
        if (currentIteration < numIterations)
        {
            for (int iTest = 0, iTestLen = tests.Length;
                 iTest < iTestLen;
                 iTest++)
            {
                tests[iTest].Test();
            }
            currentIteration++;

            // Done, stop running
            if (currentIteration == numIterations)
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                Application.Quit();
            }
            else
            {
                // Try to flush memory for the next run
                System.GC.Collect();
            }
        }
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
