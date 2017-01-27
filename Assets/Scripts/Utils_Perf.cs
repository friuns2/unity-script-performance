using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LongswordStudios
{
    public class Utils_Perf
    {
        // Texture notes:
        // Make sure Read/Write is disabled
        // Disable mipmaps if possible
        // Make sure textures are Compressed
        // Ensure sizes aren't too large
        // - 2048x2048 or 1024x1024 for UI atlases
        // - 512x512 for mobile model textures

        // Unite Europe 2016 - Optimizing Mobile Applications
        // https://www.youtube.com/watch?v=j4YAY36xjwE
        //
        // Unity US 2016 - Let's Talk (Content) Optimization
        // https://www.youtube.com/watch?v=n-oZa4Fb12U

        // Static values with no accessor functions to slow them down.
        // About 4x faster than Vector3.zero
        public static readonly Vector3 vec3_back = Vector3.back;
        public static readonly Vector3 vec3_down = Vector3.down;
        public static readonly Vector3 vec3_forward = Vector3.forward;
        public static readonly Vector3 vec3_left = Vector3.left;
        public static readonly Vector3 vec3_right = Vector3.right;
        public static readonly Vector3 vec3_up = Vector3.up;
        public static readonly Vector3 vec3_one = Vector3.one;
        public static readonly Vector3 vec3_zero = Vector3.zero;

        // Static values with no accessor functions to slow them down.
        // About 4x faster than Quaternion.identity
        public static readonly Quaternion quat_identity = Quaternion.identity;

        // Does s1 start with s2?
        // 
        // Strict byte for byte comparison, nothing fancy.
        // About 100x as fast as String.EndsWith()
        public static bool StartsWith(string s1, string s2)
        {
            return StartsWith(s1, s2, 0);
        }

        public static bool StartsWith(string s1, string s2, int startIndex)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;

            // We need the index end
            int s1Len = s1.Length - 1;
            int s2Len = s2.Length - 1;

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

        // Does s1 end with s2?
        // Strict byte for byte comparison, nothing fancy.
        // About 100x as fast as String.EndsWith()
        public static bool EndsWith(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1) || string.IsNullOrEmpty(s2))
                return false;

            int s1Len = s1.Length - 1;
            int s2Len = s2.Length - 1;

            // Too short?
            if (s1Len < s2Len)
                return false;

            for (int iChar = 0; iChar < s2Len; iChar++)
            {
                if (s1[s1Len - iChar] != s2[s2Len - iChar])
                    return false;
            }

            return true;
        }

        // Equals(s1) vs. Equals(s1, StringComparison.Ordinal)
        // Regular String.Equals() is much faster. The Ordinal
        // one calls into String.Compare() for some reason
        // which is much slower. Bizarre.

        // Byte by byte comparison
        // Doesn't use language 
        // About 3x faster than String.Contains()
        public static bool Contains(string s1, string s2)
        {
            if (string.IsNullOrEmpty(s1))
                return false;

            // s1 always contains an empty string
            // Matching String.Contains() behavior
            if (string.IsNullOrEmpty(s2))
                return true;

            int s1Len = s1.Length - 1;
            int s2Len = s2.Length - 1;
            int iChar2 = 0;
            bool match = false;

            for (int iChar1 = 0; iChar1 < s1Len; iChar1++)
            {
                // Found a possible match
                if (s1[iChar1] == s2[0])
                {
                    match = true;

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

        // This code prevents the GC allocations when using Enum as
        // a key in a Dictionary or other collection.
        //
        // Use :
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