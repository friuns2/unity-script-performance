// Revised BSD License text at bottom
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GP.Utils
{
    /// <summary>
    /// A collection of helpers for the non-allocating 
    /// Physics functions like RayCastNonAlloc()
    /// 
    /// Example :
    /// 
    /// // Note : This rhd object must be reused or there's 
    /// // no point in using the non-alloc versions. 
    /// RaycastHitData rhd = new RaycastHitData(10);
    /// if (RaycastHelper.Raycast
    ///     (rhd, Vector3.zero, Vector3.forward, 100f))
    /// {
    ///     // We hit something!
    ///     RaycastHelper.SortByDistance(rhd);
    ///     Debug.Log("Closest hit = " + rhd.hitResults[0].collider.gameObject.name);
    /// }
    /// 
    /// </summary>
    public class RaycastHelper
    {
        /// <summary>
        /// Used to sort RaycastHit by distance
        /// </summary>
        public class RHDistanceComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                return x.distance.CompareTo(y.distance);
            }
        }

        /// <summary>
        /// Used to sort RaycastHit by distance (reversed)
        /// </summary>
        public class RHDistanceRevComparer : IComparer<RaycastHit>
        {
            public int Compare(RaycastHit x, RaycastHit y)
            {
                return y.distance.CompareTo(x.distance);
            }
        }

        static IComparer<RaycastHit> _distComparer = new RHDistanceComparer();
        static IComparer<RaycastHit> _distRevComparer = new RHDistanceRevComparer();

        /// <summary>
        /// Sorts RaycastHitData by RaycastHit distance
        /// Closest is index 0, furthest is rhd.numHits
        /// 
        /// Does not allocate memory (or at least it shouldn't...)
        /// </summary>
        /// <param name="rhd"></param>
        public static void SortByDistance(RaycastHitData rhd)
        {
            Array.Sort(rhd.hitResults,
                0,
                rhd.numHits,
                _distComparer);
        }

        /// <summary>
        /// Sorts RaycastHitData by RaycastHit distance, in reverse
        /// Furthest is index 0, closest is rhd.numHits
        /// 
        /// Does not allocate memory (or at least it shouldn't...)
        /// </summary>
        /// <param name="rhd"></param>
        public static void SortByDistanceRev(RaycastHitData rhd)
        {
            Array.Sort(rhd.hitResults,
                0,
                rhd.numHits,
                _distRevComparer);
        }

        /// <summary>
        /// Check data for basic initialization
        /// </summary>
        /// <param name="rhd"></param>
        static void CheckRHDInit(RaycastHitData rhd)
        {
            if (rhd.hitResults == null ||
                rhd.hitResults.Length == 0)
            {
                rhd.Init(rhd.sizeAtInit);
            }
        }

        /// <summary>
        /// Check data for basic initialization
        /// </summary>
        /// <param name="chd"></param>
        static void CheckCHDInit(ColliderHitData chd)
        {
            if (chd.collResults == null ||
                chd.collResults.Length == 0 ||
                chd.collResults[0] == null)
            {
                chd.Init(chd.sizeAtInit);
            }
        }

        /// <summary>
        /// Function will cannot be called with a null 'results'
        /// parameter to determine how many results are REALLY
        /// available. So, if the number of hits is exactly
        /// the length of the array I resize the array and
        /// call again to be sure.
        /// </summary>
        /// <param name="rhd"></param>
        /// <returns>True if Cast needs to be called again</returns>
        static bool CheckCastResultsRHD(RaycastHitData rhd)
        {
            if (rhd.autoResize &&
                rhd.numHits == rhd.hitResults.Length)
            {
                System.Array.Resize<RaycastHit>(ref rhd.hitResults, rhd.hitResults.Length * 2);
                return false;
            }

            return true;
        }

        /// <summary>
        /// Function will cannot be called with a null 'results'
        /// parameter to determine how many results are REALLY
        /// available. So, if the number of hits is exactly
        /// the length of the array I resize the array and
        /// call again to be sure.
        /// </summary>
        /// <param name="chd"></param>
        /// <returns>True if Cast needs to be called again</returns>
        static bool CheckCastResultsCHD(ColliderHitData chd)
        {
            if (chd.autoResize &&
                chd.numHits == chd.collResults.Length)
            {
                System.Array.Resize<Collider>(ref chd.collResults, chd.collResults.Length * 2);
                return false;
            }

            return true;
        }

        /// <summary>
        /// SORT ORDER IS NOT GUARANTEED BY PHYSICS
        /// Use : RaycastHelper.SortByDistance(rhd);
        /// 
        /// https://docs.unity3d.com/ScriptReference/Physics.RaycastNonAlloc.html
        /// </summary>
        /// <param name="rhd"></param>
        /// <param name="origin"></param>
        /// <param name="direction"></param>
        /// <param name="maxDistance"></param>
        /// <returns>True if hits returned something</returns>
        public static bool Raycast
            (RaycastHitData rhd, 
             Vector3 origin, 
             Vector3 direction, 
             float maxDistance)
        {
            CheckRHDInit(rhd);

            for (; ; )
            {
                rhd.numHits = Physics.RaycastNonAlloc
                    (origin,
                     direction,
                     rhd.hitResults,
                     maxDistance,
                     rhd.layerMask,
                     rhd.triggerInteraction);

                if (CheckCastResultsRHD(rhd))
                    break;
            }

            return (rhd.numHits > 0);
        }

        /// <summary>
        /// SORT ORDER IS NOT GUARANTEED BY PHYSICS
        /// Use : RaycastHelper.SortByDistance(rhd);
        ///
        /// https://docs.unity3d.com/ScriptReference/Physics.RaycastNonAlloc.html
        /// </summary>
        /// <param name="rhd"></param>
        /// <param name="ray"></param>
        /// <param name="maxDistance"></param>
        /// <returns>True if hits returned something</returns>
        public static bool Raycast
            (RaycastHitData rhd,
             Ray ray,
             float maxDistance)
        {
            CheckRHDInit(rhd);

            // For debugging
            //Debug.DrawRay(ray.origin, ray.direction, Color.red);

            for (; ; )
            {
                rhd.numHits = Physics.RaycastNonAlloc
                    (ray,
                     rhd.hitResults,
                     maxDistance,
                     rhd.layerMask,
                     rhd.triggerInteraction);

                if (CheckCastResultsRHD(rhd))
                    break;
            }

            return (rhd.numHits > 0);
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Physics.OverlapSphereNonAlloc.html
        /// </summary>
        /// <param name="chd"></param>
        /// <param name="position"></param>
        /// <param name="radius"></param>
        /// <returns>True, if cast hits something</returns>
        public static bool OverlapSphere 
            (ColliderHitData chd, 
             Vector3 position, 
             float radius)
        {
            CheckCHDInit(chd);

            for (; ; )
            {
                chd.numHits = Physics.OverlapSphereNonAlloc
                    (position,
                     radius,
                     chd.collResults,
                     chd.layerMask,
                     chd.triggerInteraction);

                if (CheckCastResultsCHD(chd))
                    break;
            }

            return (chd.numHits>0);
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Physics.OverlapBoxNonAlloc.html
        /// </summary>
        /// <param name="chd"></param>
        /// <param name="center"></param>
        /// <param name="halfExtends"></param>
        /// <param name="orientation"></param>
        /// <returns>True, if cast hits something</returns>
        public static bool OverlapBox
            (ColliderHitData chd,
             Vector3 center,
             Vector3 halfExtends,
             Quaternion orientation)
        {
            CheckCHDInit(chd);

            for (; ; )
            {
                chd.numHits = Physics.OverlapBoxNonAlloc
                    (center,
                     halfExtends,
                     chd.collResults,
                     orientation,
                     chd.layerMask,
                     chd.triggerInteraction);

                if (CheckCastResultsCHD(chd))
                    break;
            }

            return (chd.numHits > 0);
        }

        /// <summary>
        /// https://docs.unity3d.com/ScriptReference/Physics.OverlapCapsuleNonAlloc.html
        /// </summary>
        /// <param name="chd"></param>
        /// <param name="point0"></param>
        /// <param name="point1"></param>
        /// <param name="radius"></param>
        /// <returns>True, if cast hits something</returns>
        public static bool OverlapCapsule
            (ColliderHitData chd,
             Vector3 point0,
             Vector3 point1,
             float radius)
        {
            CheckCHDInit(chd);

            for (; ; )
            {
                chd.numHits = Physics.OverlapCapsuleNonAlloc
                    (point0,
                     point1,
                     radius,
                     chd.collResults,
                     chd.layerMask,
                     chd.triggerInteraction);

                if (CheckCastResultsCHD(chd))
                    break;
            }

            return (chd.numHits > 0);
        }


        /// <summary>
        /// SORT ORDER IS NOT GUARANTEED BY PHYSICS
        /// Use : RaycastHelper.SortByDistance(rhd);
        /// 
        /// Like a raycast with thickness, or a capsule.
        /// 
        /// https://docs.unity3d.com/ScriptReference/Physics.SphereCastNonAlloc.html
        /// 
        /// From Physics.SphereCastNonAlloc : 
        /// It will only compute as many hits as fit into the buffer,
        /// and store them in no particular order. It's not 
        /// guaranteed that it will store only the closest hits.
        /// </summary>
        /// <param name="rhd">The returned hit data will be stored here</param>
        /// <param name="origin"></param>
        /// <param name="radius">The radius of the sphere</param>
        /// <param name="direction"></param>
        /// <returns>Returns true if cast hits something</returns>
        public static bool SphereCast
            (RaycastHitData rhd,
             Vector3 origin,
             float radius,
             Vector3 direction,
             float maxDistance = Mathf.Infinity)
        {
            if ( radius == 0 )
            {
                Debug.LogError("No radius, unpredicatble results");
                return false;
            }

            CheckRHDInit(rhd);

            for (; ; )
            {
                rhd.numHits = Physics.SphereCastNonAlloc
                    (origin,
                        radius,
                        direction,
                        rhd.hitResults,
                        maxDistance,
                        rhd.layerMask,
                        rhd.triggerInteraction);

                if (CheckCastResultsRHD(rhd))
                    break;

            }

            return (rhd.numHits > 0);
        }

        /// <summary>
        /// SORT ORDER IS NOT GUARANTEED BY PHYSICS
        /// Use : RaycastHelper.SortByDistance(rhd);
        /// 
        /// https://docs.unity3d.com/ScriptReference/Physics.BoxCastNonAlloc.html
        /// </summary>
        /// <param name="rhd">The returned hit data will be stored here</param>
        /// <param name="center"></param>
        /// <param name="halfExtents"></param>
        /// <param name="direction"></param>
        /// <param name="orientation"></param>
        /// <param name="maxDistance"></param>
        /// <returns>Returns true if cast hits something</returns>
        public static bool BoxCast
            (RaycastHitData rhd,
             Vector3 center,
             Vector3 halfExtents,
             Vector3 direction,
             Quaternion orientation,
             float maxDistance = Mathf.Infinity)
        {
            CheckRHDInit(rhd);

            for (; ; )
            {
                rhd.numHits = Physics.BoxCastNonAlloc
                    (center,
                     halfExtents,
                     direction,
                     rhd.hitResults,
                     orientation,
                     maxDistance,
                     rhd.layerMask,
                     rhd.triggerInteraction);

                if (CheckCastResultsRHD(rhd))
                    break;

            }

            return (rhd.numHits > 0);
        }

        /// <summary>
        /// SORT ORDER IS NOT GUARANTEED BY PHYSICS
        /// Use : RaycastHelper.SortByDistance(rhd);
        /// 
        /// https://docs.unity3d.com/ScriptReference/Physics.CapsuleCastNonAlloc.html
        /// </summary>
        /// <param name="rhd"></param>
        /// <param name="point1"></param>
        /// <param name="point2"></param>
        /// <param name="radius"></param>
        /// <param name="direction"></param>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public static bool CapsuleCast
            (RaycastHitData rhd,
             Vector3 point1,
             Vector3 point2,
             float radius,
             Vector3 direction,
             float maxDistance = Mathf.Infinity)
        {
            CheckRHDInit(rhd);

            for (; ; )
            {
                rhd.numHits = Physics.CapsuleCastNonAlloc
                    (point1,
                     point2,
                     radius, 
                     direction,
                     rhd.hitResults,
                     maxDistance,
                     rhd.layerMask,
                     rhd.triggerInteraction);

                if (CheckCastResultsRHD(rhd))
                    break;
            }

            return (rhd.numHits > 0);
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
