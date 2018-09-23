// Revised BSD License text at bottom
using System.Collections;
using System;
using System.Collections.Generic;

namespace GP.Utils
{
    /// <summary>
    /// This is an EXPERIMENTAL collection similar to a List<>
    /// but faster. The idea is to have a collection with the
    /// convenience of a List<> but more speed like an array.
    /// 
    /// USE THE CUSTOM VERSIONS IF THEY MATCH YOUR NEEDS.
    /// i.e. Use FastListInt, not FastList<int>
    ///
    /// This class is as fast or faster than 
    /// List, ArrayList, LinkedList for all uses, 
    /// if custom versions are used where needed. 
    /// 
    /// The regular generic T version uses .Equals() 
    /// which is usually much SLOWER than == for a basic type.
    /// So at the bottom I have included custom list types
    /// which are much FASTER than the generic version. You
    /// can also implement your own versions by deriving from 
    /// FastList the same way.
    /// 
    /// FastList implements a generic array. 
    /// It will grow automatically on Add() but not shrink
    /// unless Resize() is specifically called. This is to reduce Garbage 
    /// Collection performance hits, just like an array.
    /// 
    /// The disadvantage of FastList over a regular List
    /// is that this can allocate more memory and not release
    /// it until instructed. Of course, that's the whole point.
    /// 
    /// See also:
    /// C# IList : https://msdn.microsoft.com/en-us/library/system.collections.ilist(v=vs.110).aspx
    /// Foreach : https://docs.google.com/document/d/1daaIK8k7PTdYZetywLo1WAnAZv_zfMHOn15s_Px6VHc/
    /// GameObject == : https://blogs.unity3d.com/2014/05/16/custom-operator-should-we-keep-it/
    /// 
    /// If you are careful, you can use RawArray() to perform operations
    /// on the underlying array elements. For example, you can sort the
    /// array much faster than List using :
    /// System.Array.Sort(fastList.RawArray(), 0, fastList.Count);
    /// 
    /// </summary>
    public class FastList<T> : IList<T>
    {
        /// <summary>
        /// Contents of the collection
        /// </summary>
        protected T[] _entries;

        /// <summary>
        /// Number of items added to the array (not the length)
        /// Use Count to access.
        /// </summary>
        protected int _count;

        /// <summary>
        /// When re-allocating the array, multiply the existing size by this size 
        /// </summary>
        public static int ALLOCATION_INCREMENT = 2;

        /// <summary>
        /// Initial size of a new collection
        /// </summary>
        public const int START_SIZE = 16;

        /// <summary>
        /// Length of the internal array 
        /// (not the Count of the elements in the list)
        /// </summary>
        public int Capacity
        {
            get
            {
                if (_entries == null)
                    return 0;
                return _entries.Length;
            }
        }

        /// <summary>
        /// Returns the normally protected array. 
        /// 
        /// You can improve iteration performance if you use
        /// this raw array when iterating 
        /// (without adding/removing items).
        /// 
        /// Warning : modifying it may invalidate the FastList
        /// </summary>
        /// <returns></returns>
        public T[] RawArray()
        {
            return _entries;
        }

        /// <summary>
        /// Allocates a new array of default size START_SIZE
        /// </summary>
        /// <param name="size">Length of the array</param>
        public FastList(int size = START_SIZE)
        {
            if ( size != 0 )
                Resize(size);
            _count = 0;
        }

        public bool IsFixedSize
        {
            get { return false; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public int Count
        {
            get { return _count; }
        }

        public bool IsSynchronized
        {
            get { return false; }
        }

        public object SyncRoot
        {
            get { return this; }
        }

        public T GetAt (int index)
        {
            return _entries[index];
        }

        public T this[int index]
        {
            get { return _entries[index]; }
            set
            {
                _entries[index] = value;
                if (index > _count)
                    _count = index + 1;
            }
        }

        public void Add(T value)
        {
            // Allocate if needed
            if ( _count == _entries.Length )
            {
                Resize(_entries.Length * ALLOCATION_INCREMENT);
            }

            _entries[_count] = value;
            _count++;
        }

        // Don't de-allocate, don't hit GC
        public void Clear()
        {
            System.Array.Clear(_entries, 0, _entries.Length);
            _count = 0;
        }

        public bool Contains(T value)
        {
            return (IndexOf(value) == -1)?false:true;
        }

        virtual public int IndexOf(T value)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_entries[i].Equals(value))
                {
                    return i;
                }
            }
            return -1;
        }

        // TODO - need to handle inserting beyond the current capacity
        public void Insert(int index, T value)
        {
            // 0 1 2 3 4 5 6 7 8 9 = 10
            // Allocate if needed
            if (_count == _entries.Length)
            {
                Resize(_entries.Length * ALLOCATION_INCREMENT);
            }

            // Move everything down one
            // MUCH faster than a for() loop
            System.Array.Copy(_entries, index, _entries, index + 1, _entries.Length - index - 1);
            _entries[index] = value;
            _count++;
        }

        public bool Remove(T value)
        {
            int index = IndexOf(value);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            // Move everything up one
            if ((index >= 0) && (index < _count))
            {
                // MUCH faster than a for() loop
                System.Array.Copy(_entries, index + 1, _entries, index, _count - index );
                _count--;
            }
        }

        // ICollection Members
        /// <summary>
        /// Copies ALL elements to the specified array 
        /// starting at the specified destination array index. 
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Starting destination index</param>
        /// <returns></returns>
        public void CopyTo(Array array, int index)
        {
            System.Array.Copy(_entries, 0, array, index, _count);
        }

        /// <summary>
        /// Copies ALL elements to the specified array 
        /// starting at the specified destination array index. 
        /// </summary>
        /// <param name="array">Destination array</param>
        /// <param name="index">Starting destination index</param>
        /// <returns></returns>
        public void CopyTo (T[] array, int index)
        {
            System.Array.Copy(_entries, 0, array, index, _count);
        }

        /// <summary>
        /// Copies ALL elements to the specified array 
        /// starting at the specified destination array index. 
        /// </summary>
        /// <param name="list">Destination list</param>
        /// <param name="index">Starting destination index</param>
        /// <returns></returns>
        public void CopyTo(FastList<T> list, int index)
        {
            if (list.Capacity < _count)
                list.Resize(_count);

            System.Array.Copy(_entries, 0, list._entries, index, _count);
        }

        public FastListEnumerator<T> GetEnumerator()
        {
            FastListEnumerator<T> fastEnum = new FastListEnumerator<T>();
            fastEnum._list = this;
            fastEnum.Reset();
            return fastEnum;
        }

        // Needed as well as <T> version
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)_entries).GetEnumerator();
        }

        // IEnumerable Members
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            FastListEnumerator<T> fastEnum = new FastListEnumerator<T>();
            fastEnum._list = this;
            fastEnum.Reset();
            return (IEnumerator<T>)fastEnum;
        }

        // CUSTOM FUNCTIONS
        public void Resize(int newLength)
        {
            System.Array.Resize(ref _entries, newLength);
        }

        //void ICollection<T>.Add(T item)
        //{
        //    ((ICollection<T>)_entries).Add(item);
        //}

        //IEnumerator<T> IEnumerable<T>.GetEnumerator()
        //{
        //    return ((ICollection<T>)_entries).GetEnumerator();
        //}

        public struct FastListEnumerator<E> : IEnumerator<E>
        {
            public int _index;
            public FastList<E> _list;
            private E _current;

            public E Current
            {
                get { return _current; }
            }

            object IEnumerator.Current
            {
                get { return (object)_current; }
            }

            public bool MoveNext()
            {
                if (_index < _list._count - 1)
                {
                    _index++;
                    _current = _list[_index];
                    return true;
                }
                else
                    return false;
            }

            public void Reset()
            {
                _index = -1;
            }

            void IDisposable.Dispose()
            {
            }
        }
    }

    /// <summary>
    /// Custom classes for FastList
    /// These exist for types where the .Equals operation is slower than ==
    /// 
    /// Use these classes (if available) if you use the search
    /// functions Contains(), IndexOf(), or Remove(T value)
    /// </summary>
    public class FastListInt : FastList<int>
    {
        public FastListInt() : base()
        {
        }

        public FastListInt(int size) : base(size)
        {
        }

        override public int IndexOf(int value)
        {
            int itemIndex = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_entries[i] == value)
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
        }
    }

    public class FastListBool : FastList<bool>
    {
        public FastListBool(int size = START_SIZE) : base(size)
        {
        }

        override public int IndexOf(bool value)
        {
            int itemIndex = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_entries[i] == value)
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
        }
    }

    public class FastListFloat : FastList<float>
    {
        public FastListFloat(int size = START_SIZE) : base(size)
        {
        }

        override public int IndexOf(float value)
        {
            int itemIndex = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_entries[i] == value)
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
        }
    }

    public class FastListGO : FastList<UnityEngine.GameObject>
    {
        public FastListGO(int size = START_SIZE) : base(size)
        {
        }

        override public int IndexOf(UnityEngine.GameObject value)
        {
            int itemIndex = -1;
            for (int i = 0; i < _count; i++)
            {
                if (_entries[i] == value)
                {
                    itemIndex = i;
                    break;
                }
            }
            return itemIndex;
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
