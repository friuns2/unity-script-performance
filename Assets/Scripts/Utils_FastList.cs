using UnityEngine;
using System.Collections;
using System;

namespace LongswordStudios
{
    /// <summary>
    /// This is an EXPERIMENTAL collection similar to a List<>
    /// but faster. The idea is to have a collection with the
    /// convenience of a List<> but more speed like an Array.
    /// 
    /// It implements a Generic array and an array
    /// of booleans which tracks which entries are being used.
    /// It will grow automatically on Add() but not shrink
    /// unless specifically called. This is to reduce Garbage 
    /// Collection performance hits, like an array.
    /// </summary>
    public class Utils_FastList<T>
    {
        public bool[] _validEntries;
        public T[] _entries;
        public int _length;
        public int _firstEmptyEntry;
        static int sizeIncrement = 100;
        IEquatable<T> compFunc;
        TypeCode typeCode;

        public Utils_FastList(int size)
        {
            _length = size;
            _entries = new T[_length];
            _validEntries = new bool[_length];
            _firstEmptyEntry = 0;
            compFunc = _entries[0] as IEquatable<T>;
            typeCode = Type.GetTypeCode(typeof(T));
        }

        /// <summary>
        /// Is there a value at this index?
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool IsValid(int index)
        {
            return _validEntries[index];
        }

        /// <summary>
        /// Check IsValid() first.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T GetAt(int index)
        {
            return _entries[index];
        }

        /// <summary>
        /// For some data types this function can allocate memory!
        /// Use Utils_Collections.Contains() FIRST if that type exists.
        /// 
        /// The Equals() function allocates for some types (Int32!)
        /// Only use this for types that do not allocate
        /// This is slightly faster than List<GameObject>.Contains()
        /// 3.63 ms vs. 4.07 ms for 100 objects
        /// Slightly faster for GameObjects over List<>
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public bool Contains(T val)
        {
            for (int i = 0, iLen = _length;
                  i < iLen;
                  i++)
            {
                if (_validEntries[i] &&
                    _entries[i].Equals(val))
                    return true;
            }
            return false;
        }

        public void Clear()
        {
            System.Array.Clear(_validEntries, 0, _length);
            System.Array.Clear(_entries, 0, _length);
            _firstEmptyEntry = 0;
        }

        /// <summary>
        /// For some data types this function can allocate memory!
        /// Use Utils_Collections.Contains() FIRST if that type exists.
        ///
        /// Removes the first value matching val
        /// </summary>
        /// <param name="val">Value to match</param>
        /// <returns></returns>
        public bool Remove(T val)
        {
            for (int i = 0; i < _length; i++)
            {
                if (_validEntries[i] &&
                    _entries[i].Equals(val))
                {
                    _validEntries[i] = false;
                    System.Array.Clear(_entries, i, 1);

                    // This new empty slot occurs
                    // earlier than the last one
                    if (i < _firstEmptyEntry)
                        _firstEmptyEntry = i;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Allocates about as much as List<T> plus
        /// the boolean array.
        /// About as fast as List<T>.Add() + ~10%
        /// </summary>
        /// <param name="val">Value to add to the collection.</param>
        public void Add(T val)
        {
            // Find an unused location

            // There's an empty location
            if (_firstEmptyEntry < _length)
            {
                _validEntries[_firstEmptyEntry] = true;
                _entries[_firstEmptyEntry] = val;

                _firstEmptyEntry++;
                // Don't re-size even if full until
                // the next Add()
            }
            else
            {
                // No empties, resize and add
                Resize(_length + sizeIncrement);

                _validEntries[_firstEmptyEntry] = true;
                _entries[_firstEmptyEntry] = val;
                // Next valid will be 1 ahead of the old
                // array length
                _firstEmptyEntry++;

            }
        }

        public void Resize(int newLength)
        {
            System.Array.Resize(ref _validEntries, newLength);
            System.Array.Resize(ref _entries, newLength);
            _length = newLength;
        }

        /// <summary>
        /// First the first unused index in the array.
        /// </summary>
        /// <param name="startIndex"></param>
        public void FindEmpty(int startIndex)
        {
            // Set to an invalid position
            _firstEmptyEntry = _entries.Length;

            for (int i = startIndex, iLen = _entries.Length;
                 i < iLen;
                 i++)
            {
                if (_validEntries[i] == false)
                {
                    _firstEmptyEntry = i;
                    break;
                }
            }
        }
    }

    public class Utils_Collections
    {
        /// <summary>
        /// NO ALLOCATIONS
        /// MUCH faster than List<int>.Contains()
        /// .35 ms vs. 10.05 ms for 100 objects
        /// </summary>
        /// <param name="fl"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool Contains(Utils_FastList<Int32> fl, Int32 val)
        {
            for (int i = 0, iLen = fl._length;
                i < iLen;
                i++)
            {
                if (fl._validEntries[i] &&
                     fl._entries[i] == val)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// NO ALLOCATIONS
        /// Removes the first value matching val
        /// MUCH faster than List<int>.Remove()
        /// 2.91 ms vs. 26.59 ms for 500 items
        /// </summary>
        /// <param name="fl"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool Remove(Utils_FastList<Int32> fl, Int32 val)
        {
            for (int i = 0; i < fl._length; i++)
            {
                if (fl._validEntries[i] &&
                    fl._entries[i] == val)
                {
                    fl._validEntries[i] = false;
                    fl._entries[i] = 0;

                    // This new empty slot occurs
                    // earlier than the last one
                    if (i < fl._firstEmptyEntry)
                        fl._firstEmptyEntry = i;
                    return true;
                }
            }
            return false;
        }
    }
}

