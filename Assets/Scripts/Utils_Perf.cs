// Revised BSD License text at bottom
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GP.Utils
{
    // Unite Europe 2016 - Optimizing Mobile Applications
    // https://www.youtube.com/watch?v=j4YAY36xjwE
    //
    // Unity US 2016 - Let's Talk (Content) Optimization
    // https://www.youtube.com/watch?v=n-oZa4Fb12U

    /// <summary>
    /// Performance utility code
    /// </summary>
    public class Utils_Perf
    {
        /// <summary>
        /// Static values with no accessor functions to slow them down. 
        /// About 4x faster than Vector3.zero
        /// </summary>
        public static readonly Vector3 vec3_back = Vector3.back;
        public static readonly Vector3 vec3_down = Vector3.down;
        public static readonly Vector3 vec3_forward = Vector3.forward;
        public static readonly Vector3 vec3_left = Vector3.left;
        public static readonly Vector3 vec3_right = Vector3.right;
        public static readonly Vector3 vec3_up = Vector3.up;
        public static readonly Vector3 vec3_one = Vector3.one;
        public static readonly Vector3 vec3_zero = Vector3.zero;

        /// <summary>
        /// Static values with no accessor functions to slow them down.
        /// About 4x faster than Quaternion.identity
        /// </summary>
        public static readonly Quaternion quat_identity = Quaternion.identity;

        /// <summary>
        /// Does s1 start with s2?
        /// Strict byte for byte comparison, nothing fancy.
        /// About 100x as fast as String.StartsWith()
        /// </summary>
        /// <param name="s1">Longer string</param>
        /// <param name="s2">Shorter string</param>
        /// <returns>true, if s1 starts with s2</returns>
        public static bool StartsWith(string s1, string s2)
        {
            return StartsWith(s1, s2, 0);
        }

        /// <summary>
        /// Does s1 start with s2?
        /// Strict byte for byte comparison, nothing fancy.
        /// About 100x as fast as String.StartsWith()
        /// </summary>
        /// <param name="s1">Longer string</param>
        /// <param name="s2">Shorter string</param>
        /// <param name="startIndex">Start checking at this index of s1</param>
        /// <returns>true, if s1 starts with s2</returns>
        public static bool StartsWith(string s1, string s2, int startIndex)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;

            // We need the index end
            int s1Len = s1.Length;
            int s2Len = s2.Length;

            // Too short?
            if (s1Len < s2Len)
                return false;

            for (int iChar = startIndex; iChar < s2Len; iChar++)
            {
                if (s1[iChar] != s2[iChar])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Does s1 end with s2?
        /// Strict byte for byte comparison, nothing fancy.
        /// About 100x as fast as String.EndsWith()
        /// </summary>
        /// <param name="s1">Longer string</param>
        /// <param name="s2">Shorter string</param>
        /// <returns>true, if s1 ends with s2</returns>        
        public static bool EndsWith(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;

            int s1Len = s1.Length - 1;
            int s2Len = s2.Length - 1;

            // Too short?
            if (s1Len < s2Len)
                return false;

            for (int iChar = 0; iChar <= s2Len; iChar++)
            {
                if (s1[s1Len - iChar] != s2[s2Len - iChar])
                    return false;
            }

            return true;
        }

        /// <summary>
        /// About 3x faster than String.Contains()
        /// 
        /// Byte by byte comparison
        /// Doesn't use language features of C#
        /// 
        /// With Equals(s1) vs. Equals(s1, StringComparison.Ordinal)
        /// regular String.Equals() is much faster. The Ordinal
        /// one calls into String.Compare() for some reason
        /// which is much slower. Bizarre.
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        /// <returns>true, if s1 contains s2 in it anywhere.</returns>
        public static bool Contains(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1))
                return false;

            // s1 always contains an empty string
            // Matching String.Contains() behavior
            if (string.IsNullOrEmpty(s2))
                return true;

            // Example:
            // 0123456789
            // 789
            // We only need to check the first string up to
            // the last 3 characters.

            // Length we need to check the first string
            int s1Len = s1.Length - s2.Length + 1;
            int s2Len = s2.Length;
            int iChar2 = 0;
            bool match = false;

            for (int iChar1 = 0; iChar1 < s1Len; iChar1++)
            {
                // Found a possible match
                if (s1[iChar1] == s2[0])
                {
                    match = true;

                    // Loop until we find a mismatch.
                    // If we don't, then it matches.
                    for (iChar2 = 0; iChar2 < s2Len; iChar2++)
                    {
                        if (s1[iChar1 + iChar2] != s2[iChar2])
                        {
                            match = false;
                            break;
                        }
                    }

                    if (match)
                        return true;
                }
            }
            return false;
        }

        // Usage :
        //
        // public enum TEST_KEY
        // {
        //  thing1,
        //  thing2,
        //  thing3
        // }
        // Utils_Perf.EnumIntEqComp<TEST_KEY> noboxCompare;
        // Dictionary<TEST_KEY, whatever> dictEnumIntNoBox = 
        //      new Dictionary<TEST_KEY, whatever>(noboxCompare);
        //
        // https://stackoverflow.com/questions/26280788/dictionary-enum-key-performance
        // todo; check if your TEnum is enum && typeCode == TypeCode.Int

        /// <summary>
        /// This code prevents the GC allocations when using Enum as
        /// a key in a Dictionary or other collection.
        /// </summary>
        /// <typeparam name="TEnum">Key type</typeparam>
        public struct EnumIntEqComp<TEnum> : IEqualityComparer<TEnum>
            where TEnum : struct
        {
            public static class BoxAvoidance
            {
                public static readonly System.Func<TEnum, int> _wrapper;

                public static int ToInt(TEnum enu)
                {
                    return _wrapper(enu);
                }

                static BoxAvoidance()
                {
                    var p = System.Linq.Expressions.Expression.Parameter(typeof(TEnum), null);
                    var c = System.Linq.Expressions.Expression.ConvertChecked(p, typeof(int));

                    _wrapper = System.Linq.Expressions.Expression.Lambda<System.Func<TEnum, int>>(c, p).Compile();
                }
            }

            public bool Equals(TEnum firstEnum, TEnum secondEnum)
            {
                return BoxAvoidance.ToInt(firstEnum) ==
                    BoxAvoidance.ToInt(secondEnum);
            }

            public int GetHashCode(TEnum firstEnum)
            {
                return BoxAvoidance.ToInt(firstEnum);
            }
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
