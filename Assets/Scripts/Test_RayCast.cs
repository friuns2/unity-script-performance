// Revised BSD License text at bottom
using UnityEngine;
using GP.Utils;
using System;

/// <summary>
/// Test for the non-allocating Physics functions
/// </summary>
public class Test_RayCast : MonoBehaviour, ITestController
{
    public int numIterations = 1000;

    public float range = 100;

    [Header ("RaycastHitData")]
    [SerializeField]
    public RaycastHitData rhd;

    [Space(5)]
    [Header("ColliderHitData")]
    [SerializeField]
    public ColliderHitData chd;

    private LayerMask layerMask;

    public void Init()
    {
        // Reuse the layermask from RHD for standard Raycast
        layerMask = rhd.layerMask;

        if (range == 0)
            Debug.LogError("Set the range");
    }

    public void Test()
    {
        //// Example
        //RaycastHitData rhd = new RaycastHitData(10);
        //if (RaycastHelper.Raycast
        //        (rhd, Utils_Perf.vec3_zero, Utils_Perf.vec3_forward, 100f))
        //{
        //    // We hit something!
        //    RaycastHelper.SortByDistance(rhd);
        //    Debug.Log("Closest hit = " + rhd.hitResults[0].collider.gameObject.name);
        //}
        float distance = 0;

        // BACKWARDS
        UnityEngine.Profiling.Profiler.BeginSample("Raycast (Helper) backwards");
        for (int i = 0; i < numIterations; i++)
        {
            if (!RaycastHelper.Raycast
                (rhd, Utils_Perf.vec3_forward * range, Utils_Perf.vec3_back, range))
                Debug.LogError("Bug!");
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Raycast (SortResults backwards)");
        RaycastHelper.SortByDistanceRev(rhd);
        UnityEngine.Profiling.Profiler.EndSample();

        distance = rhd.hitResults[0].distance;
        for (int i = 0, iLen = rhd.numHits; i < iLen; ++i)
        {
            if (distance < rhd.hitResults[i].distance)
                Debug.LogError("Bad sort");

            distance = rhd.hitResults[i].distance;
        }

        // FORWARDS
        UnityEngine.Profiling.Profiler.BeginSample("Raycast (Helper) forward");
        for (int i = 0; i < numIterations; i++)
        {
            if (!RaycastHelper.Raycast
                (rhd, Utils_Perf.vec3_zero, Utils_Perf.vec3_forward, range))
                Debug.LogError("Bug!");
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Raycast (SortResults forward)");
        RaycastHelper.SortByDistance(rhd);
        UnityEngine.Profiling.Profiler.EndSample();

        // Test sort
        distance = rhd.hitResults[0].distance;
        for ( int i = 0, iLen = rhd.numHits; i < iLen; ++i)
        {
            if (distance > rhd.hitResults[i].distance)
                Debug.LogError("Bad sort");

            distance = rhd.hitResults[i].distance;
        }




        // REGULAR
        UnityEngine.Profiling.Profiler.BeginSample("Raycast (Regular)");
        for (int i = 0; i < numIterations; i++)
        {
            RaycastHit[] hits = Physics.RaycastAll
                (Utils_Perf.vec3_zero, Utils_Perf.vec3_forward, range, layerMask);

            if (hits.Length == 0)
                Debug.LogError("Bug!");
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("SphereCast (Helper)");
        for (int i = 0; i < numIterations; i++)
        {
            if (!RaycastHelper.SphereCast
                (rhd, Utils_Perf.vec3_zero, 1f, Utils_Perf.vec3_forward, range))
                Debug.LogError("Bug!");
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Collider data
        UnityEngine.Profiling.Profiler.BeginSample("OverlapSphere (Helper)");
        for (int i = 0; i < numIterations; i++)
        {
            if (!RaycastHelper.OverlapSphere
                (chd, Utils_Perf.vec3_zero, range))
                Debug.LogError("Bug!");
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
