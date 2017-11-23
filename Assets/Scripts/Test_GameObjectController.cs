using UnityEngine;
using System.Collections;

/// <summary>
/// Test controller for the Test_GameObject class.
/// </summary>
public class Test_GameObjectController : MonoBehaviour, ITestController
{
    Test_GameObject obj;
    private int iterations = 10000;

    // Use this for initialization
    public void Init()
    {
        GameObject go = new GameObject();
        obj = go.AddComponent<Test_GameObject>();
    }

    // Update is called once per frame
    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Access GameObject (cached)");
        for (int iObj = 0; iObj < iterations; iObj++)
        {
            obj.Update_CachedGameObject();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Access GameObject (standard)");
        for (int iObj = 0; iObj < iterations; iObj++)
        {
            obj.Update_GameObject();
        }
        UnityEngine.Profiling.Profiler.EndSample();
    }
}
