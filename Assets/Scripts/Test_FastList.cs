// Revised BSD License text at bottom
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Performance test for FastList vs. regular List
/// </summary>
public class Test_FastList : MonoBehaviour, ITestController
{
    public int numIterations = 1000;

    GP.Utils.FastListInt fastListInt;
    List<int> listInt;

    public void Init()
    {
        fastListInt = new GP.Utils.FastListInt(numIterations);
        listInt = new List<int>(numIterations);
        for (int i = 0; i < numIterations; i++)
        {
            fastListInt.Add(i);
            listInt.Add(i);
        }

    }

    bool CheckArray(GP.Utils.FastListInt fastListInt, int[] correctArray)
    {
        if (fastListInt.Count != correctArray.Length)
            return false;

        for (int iCheck = 0; iCheck < correctArray.Length; iCheck++)
        {
            if (fastListInt[iCheck] != correctArray[iCheck])
                return false;
        }
        return true;
    }

    public void Test()
    {
        // Functional testing of FastList
        //
        //int[] correctArray = new int[] { 0, 1, 2, 3, 4, 5 };
        //GP.Utils.FastListInt fastListInt = new GP.Utils.FastListInt(6);
        //fastListInt.Add(0);
        //fastListInt.Add(1);
        //fastListInt.Add(2);
        //fastListInt.Add(2);
        //fastListInt.Add(3);
        //fastListInt.Add(4);
        //fastListInt.Add(5);
        //fastListInt.Add(6);
        //fastListInt.Remove(2);
        //fastListInt.Remove(6);
        //if (!CheckArray(fastListInt, correctArray))
        //    Debug.LogError("BUG");
        //fastListInt.RemoveAt (3);
        //fastListInt.Insert(3, 3);
        //if (!CheckArray(fastListInt, correctArray))
        //    Debug.LogError("BUG");

        GameObject go = new GameObject();

        // Add
        UnityEngine.Profiling.Profiler.BeginSample("FastList (add)");
        GP.Utils.FastList<GameObject> fastList = new GP.Utils.FastList<GameObject>();
        {
            for (int i = 0; i < numIterations; i++)
            {
                fastList.Add(go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (add)");
        GP.Utils.FastListGO fastListGO = new GP.Utils.FastListGO();
        {
            for (int i = 0; i < numIterations; i++)
            {
                fastListGO.Add(go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (add)");
        List<GameObject> list = new List<GameObject>();
        {
            for (int i = 0; i < numIterations; i++)
            {
                list.Add(go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Contains
        UnityEngine.Profiling.Profiler.BeginSample("FastList (contains)");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (!fastList.Contains(go))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (contains)");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (!fastListGO.Contains(go))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (contains)");
        {
            for (int i = 0; i < numIterations; i++)
            {
                if (!list.Contains(go))
                    Debug.LogError("Bug!");
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Sort
        UnityEngine.Profiling.Profiler.BeginSample("FastListInt (Sort)");
        System.Array.Sort(fastListInt.RawArray(), 0, fastListInt.Count);
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("ListInt (Sort)");
        listInt.Sort();
        UnityEngine.Profiling.Profiler.EndSample();


        // Shallow copy
        {
            UnityEngine.Profiling.Profiler.BeginSample("FastList (CopyTo)");
            for (int i = 0; i < numIterations; i++)
            {
                GP.Utils.FastList<GameObject> fastList2 = new GP.Utils.FastList<GameObject>(0);
                fastList.CopyTo(fastList2, 0);
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        {
            UnityEngine.Profiling.Profiler.BeginSample("FastListGO (CopyTo)");
            for (int i = 0; i < numIterations; i++)
            {
                GP.Utils.FastListGO fastListGO2 = new GP.Utils.FastListGO(0);
                fastListGO.CopyTo(fastListGO2, 0);
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        {
            UnityEngine.Profiling.Profiler.BeginSample("List (CopyTo)");
            for (int i = 0; i < numIterations; i++)
            {
                List<GameObject> list2 = new List<GameObject>(list);
            }
            UnityEngine.Profiling.Profiler.EndSample();
        }

        // foreach
        int iAdd = 0;
        UnityEngine.Profiling.Profiler.BeginSample("FastList (foreach)");
        foreach (GameObject go1 in fastList)
        {
            if (go1.activeSelf)
                iAdd++;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (foreach)");
        foreach (GameObject go1 in fastListGO)
        {
            if (go1.activeSelf)
                iAdd++;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (foreach)");
        foreach (GameObject go1 in list)
        {
            if (go1.activeSelf)
                iAdd++;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // for (backwards)
        UnityEngine.Profiling.Profiler.BeginSample("FastList (for)");
        for (int i = fastList.Count-1; i > 0; i--)
        {
            if (fastList[i].activeSelf)
                iAdd++;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (for)");
        for (int i = fastListGO.Count-1; i > 0; i--)
        {
            if (fastListGO[i].activeSelf)
                iAdd++;
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (for)");
        for (int i = list.Count-1; i > 0; i--)
        {
            if (list[i].activeSelf)
                iAdd++;
        }
        UnityEngine.Profiling.Profiler.EndSample();


        // RemoveAt for 1/2
        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (RemoveAt)");
        {
            for (int i = 0; i < numIterations / 2; i++)
            {
                fastListGO.RemoveAt(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (RemoveAt)");
        {
            int lCount = list.Count / 2;
            for (int i = 0; i < lCount; i++)
            {
                list.RemoveAt(i);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Remove
        UnityEngine.Profiling.Profiler.BeginSample("FastList (remove)");
        {
            int flCount = fastList.Count;
            for (int i = 0; i < flCount; i++)
            {
                fastList.Remove(go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (remove)");
        {
            int flGOCount = fastListGO.Count;
            for (int i = 0; i < flGOCount; i++)
            {
                fastListGO.Remove(go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();


        UnityEngine.Profiling.Profiler.BeginSample("List (remove)");
        {
            int lCount = list.Count;
            for (int i = 0; i < lCount; i++)
            {
                list.Remove(go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Insert
        UnityEngine.Profiling.Profiler.BeginSample("FastList (insert)");
        {
            for (int i = 0; i < numIterations; i++)
            {
                fastList.Insert(0, go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (insert)");
        {
            for (int i = 0; i < numIterations; i++)
            {
                fastListGO.Insert(0, go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (insert)");
        {
            for (int i = 0; i < numIterations; i++)
            {
                list.Insert(0, go);
            }
        }
        UnityEngine.Profiling.Profiler.EndSample();

        // Clear
        UnityEngine.Profiling.Profiler.BeginSample("FastList (clear)");
        {
            fastList.Clear();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("FastListGO (clear)");
        {
            fastListGO.Clear();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("List (clear)");
        {
            list.Clear();
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
