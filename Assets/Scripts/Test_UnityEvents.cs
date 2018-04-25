// Revised BSD License text at bottom
using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System;

/// <summary>
/// Testing Unity events
/// See also : https://jacksondunstan.com/articles/3335
/// </summary>
public class Test_UnityEvents : MonoBehaviour
{
    // An event that sends an int
    // These allocate!
    [Serializable]
    public class TestIntEvent : UnityEvent<int> {}
    public TestIntEvent evtInt = new TestIntEvent();

    // An event with no parameters
    // This does not allocate
    public UnityEvent evt = new UnityEvent();

    // Just a fake value so the function isn't
    // optimized out by the compiler.
    int numReceived = 0;

    public void Init()
    {
        //evt.AddListener(ReceiveEvent);
        evtInt.AddListener(ReceiveEventInt);
    }

    public void SendEvent()
    {
        //evt.Invoke();
        evtInt.Invoke(1);
    }

    public void ReceiveEventInt(int value)
    {
        numReceived++;
    }

    public void ReceiveEvent()
    {
        numReceived++;
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
