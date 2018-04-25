// Revised BSD License text at bottom
using UnityEngine;
using System.Collections;

public class Test_EventsController : MonoBehaviour, ITestController
{
    public int numIterations = 1000;
    Test_UnityEvents unityEvents;
    Test_Action actionEvents;
    Test_SendMessage sendMsg;
    Test_UnityMessage unityMsg;

    public void Init()
    {
        unityEvents = GetComponentInChildren<Test_UnityEvents>();
        unityEvents.Init();

        actionEvents = GetComponentInChildren<Test_Action>();
        actionEvents.Init();

        sendMsg = GetComponentInChildren<Test_SendMessage>();
        sendMsg.Init();

        unityMsg = GetComponentInChildren<Test_UnityMessage>();
        unityMsg.Init();
    }

    public void Test()
    {
        UnityEngine.Profiling.Profiler.BeginSample("Unity events");
        for (int a = 0; a < numIterations; a++)
        {
            unityEvents.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Action events");
        for (int a = 0; a < numIterations; a++)
        {
            actionEvents.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Send message");
        for (int a = 0; a < numIterations; a++)
        {
            sendMsg.SendEvent();
        }
        UnityEngine.Profiling.Profiler.EndSample();

        UnityEngine.Profiling.Profiler.BeginSample("Unity messaging");
        for (int a = 0; a < numIterations; a++)
        {
            unityMsg.SendEvent();
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
