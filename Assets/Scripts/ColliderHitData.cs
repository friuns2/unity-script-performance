// Revised BSD License text at bottom
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GP.Utils
{
    /// <summary>
    /// Data class for the non-alloc Physics helper functions in RaycastHelper
    /// </summary>
    [System.Serializable]
    public class ColliderHitData
    {
        [Header("SETUP")]
        /// <summary>
        /// A Layer mask that is used to selectively ignore colliders when casting.
        /// </summary>
        [SerializeField]
        public LayerMask layerMask = Physics.DefaultRaycastLayers;

        /// <summary>
        /// Specifies whether this query should hit Triggers.
        /// </summary>
        [SerializeField]
        public QueryTriggerInteraction triggerInteraction = QueryTriggerInteraction.UseGlobal;

        /// <summary>
        /// Will resize the hitResults array
        /// if the results array is smaller (or equal to)
        /// the number of results from Physics. You
        /// will always get ALL the hits back, not just
        /// the amount you allocated for.
        /// </summary>
        [SerializeField]
        public bool autoResize = true;

        /// <summary>
        /// The casting functions will allocate the collResults
        /// array for you if this is greater than zero.
        /// This variable makes it easier to setup in the 
        /// editor.
        /// </summary>
        [SerializeField]
        public int sizeAtInit = 0;

        [Space(10)]
        [Header("RUNTIME RESULTS")]
        /// <summary>
        /// The amount of hits stored into the results buffer.
        /// </summary>
        /// [SerializeField]
        public int numHits = 0;

        [SerializeField]
        /// <summary>
        /// Results returned from Physics calls
        /// </summary>
        public Collider[] collResults;

        public const int DEFAULT_SIZE = 16;

        public ColliderHitData()
        {
            if (sizeAtInit > 0)
                Init(sizeAtInit);
        }

        /// <summary>
        /// Initialize the collResults array
        /// </summary>
        /// <param name="size">Array size</param>
        public ColliderHitData(int size)
        {
            if (size > 0)
                Init(size);
        }

        /// <summary>
        /// Initialize the collResults array
        /// </summary>
        /// <param name="hitCapacity"></param>
        public void Init (int hitCapacity)
        {
            if (hitCapacity == 0)
                collResults = new Collider[DEFAULT_SIZE];
            else
                collResults = new Collider[hitCapacity];
            numHits = 0;
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
