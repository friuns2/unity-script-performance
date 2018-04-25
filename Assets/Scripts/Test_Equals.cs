// Revised BSD License text at bottom
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GP.Utils;

/// <summary>
/// Test speed of == vs. .Equals for various types
/// </summary>
public class Test_Equals : MonoBehaviour, ITestController
{
    public static int numIterations = 10000;

    public void Init()
    {
    }

    public void Test()
    {
        GameObject go1 = new GameObject();
        GameObject go2 = new GameObject();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (go1 == go2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (go1.Equals(go2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : !go1");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (!go1)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (GameObject) : == null");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (go1 == null)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        int i1 = 1;
        int i2 = 2;

        UnityEngine.Profiling.Profiler.BeginSample("Equals (int) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i1 == i2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (int) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (i1.Equals(i2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // String
        string s1 = "1";
        string s2 = "2";

        UnityEngine.Profiling.Profiler.BeginSample("Equals (string) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (s1.Equals(s2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (string) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (s1 == s2)
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // bool
        bool b1 = true;
        bool b2 = false;

        UnityEngine.Profiling.Profiler.BeginSample("Equals (bool) : .Equals");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (b1.Equals(b2))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Equals (bool) : ==");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (b1 == b2)
                    Debug.LogError("Bug!");
            }
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
