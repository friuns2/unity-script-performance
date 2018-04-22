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


        UnityEngine.Profiling.Profiler.BeginSample("Raycast (Helper)");
        for (int i = 0; i < numIterations; i++)
        {
            if (!RaycastHelper.Raycast
                (rhd, Utils_Perf.vec3_zero, Utils_Perf.vec3_forward, range))
                Debug.LogError("Bug!");
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Raycast (SortResults)");
        RaycastHelper.SortByDistanceRev(rhd);
        RaycastHelper.SortByDistance(rhd);
        UnityEngine.Profiling.Profiler.EndSample();

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
