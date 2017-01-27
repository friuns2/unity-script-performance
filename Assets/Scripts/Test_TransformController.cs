using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Project created by Garret Polk from Longsword Studios, Inc.
// http://www.longswordstudios.com
// Code comments are mine.
// 
// Code was taken from the slides of Søren Trautner Madsen
// from the talk/video below. soren@playdead.com
//
// https://docs.google.com/presentation/d/1dew0TynVmtQf8OMLEz_YtRxK32a_0SAZU9-vgyMRPlA
// https://www.youtube.com/watch?v=mQ2KTRn4BMI&t
//
// Performance improvements using a GameObject transform
//
public class Test_TransformController : MonoBehaviour
{
    public Test_Transform scriptToUseForSetup;
    List<Test_Transform> scriptList = new List<Test_Transform>();

    // this array will be filled with 100 elements in “Start()”
    Test_Transform[] scriptArray;

    public bool testForEachList = true;
    public bool testForList = true;
    public bool testForArray = true;

    void Start()
    {
        // Build the test data
        scriptArray = new Test_Transform[100];

        for (int i = 0; i < 100; ++i)
        {
            scriptList.Add(scriptToUseForSetup);
            scriptArray[i] = scriptToUseForSetup;
        }
    }

    void Update()
    {
        Test_Transform.globalDeltaTime = Time.deltaTime;

        // do a lot of iterations:
        for (int a = 0; a < 100; ++a)
        {
            if (testForArray)
            {
                // for() version with array
                Update_Array_Version();
            }

            if (testForList)
            {
                // for() version with List
                //
                Update_For_List_Version();
            }

            if (testForEachList)
            {
                // foreach() version with List
                //
                Update_ForEach_List_Version();
            }
        }
    }

    void Update_For_List_Version()
    {
        // cnt = scriptList.Count caches the length
        // but we do it each time to be consistent.

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_ReduceVectorOps();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_CachedTransforms();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_LocalPosition();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_ReduceEngineCalls();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_NoVectorMath();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_CacheDeltaTimeGetSet();

        for (int i = 0, cnt = scriptList.Count; i < cnt; ++i)
            scriptList[i].UpdateCharacter_CacheDeltaTime();
    }

    void Update_ForEach_List_Version()
    {
        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_ReduceVectorOps();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_CachedTransforms();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_LocalPosition();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_ReduceEngineCalls();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_NoVectorMath();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_CacheDeltaTimeGetSet();

        foreach (Test_Transform script in scriptList)
            script.UpdateCharacter_CacheDeltaTime();
    }

    void Update_Array_Version()
    {
        // cnt = scriptArray.Length caches the length
        // but we do it each time to be consistent.
        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_ReduceVectorOps();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_CachedTransforms();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_LocalPosition();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_ReduceEngineCalls();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_NoVectorMath();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_CacheDeltaTimeGetSet();

        for (int i = 0, cnt = scriptArray.Length; i < cnt; ++i)
            scriptArray[i].UpdateCharacter_CacheDeltaTime();
    }
}

