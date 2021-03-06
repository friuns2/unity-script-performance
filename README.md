# Unity Script Performance Examples #

This project is a set of examples and utilities to help you improve Unity's C# script performance.

https://bitbucket.org/GarretPolk/unity-script-performance

## FEATURES ##

* GameObject.transform improvements
* Faster string comparison functions - StartsWith(), etc.
* Faster access to common static data like Vector3.zero
* Prevent garbage collection memory allocations using Enum as a key in containers
* Speed tests for collection types (List, Dictionary, etc.) for Contains/Add/Remove().
* FastList<T> collection : Trades memory for speed, faster than List<T> using an array to hold data.
* Helper functions for non-allocating Physics 'cast' functions like RayCastNonAlloc() - See RaycastHelpers

### Details ###

Project created by Garret Polk
http://www.garretpolk.com

### Videos and slides ###
 
Transform example code was taken from the slides of S�ren Trautner Madsen from the talk/video below. soren@playdead.com

* Slides : https://docs.google.com/presentation/d/1dew0TynVmtQf8OMLEz_YtRxK32a_0SAZU9-vgyMRPlA
* Video of talk : https://www.youtube.com/watch?v=mQ2KTRn4BMI&t

Strings, accessors (get/set), and containers

* Unite Europe 2016 - Optimizing Mobile Applications : https://www.youtube.com/watch?v=j4YAY36xjwE
* Unity US 2016 - Let's Talk (Content) Optimization : https://www.youtube.com/watch?v=n-oZa4Fb12U

## USE ##

https://docs.unity3d.com/Manual/ProfilerCPU.html

1. In Unity select Window > Profiler
2. Select "Deep Profile" button at the top
* Each test int the scene is in a GameObject under "Performance Tests" and can be turned on/off individually by enable/disabling the GameObject.
3. Play the "game".
* The test will run for 10 frames, then exit.
4. Click in the the CPU Usage area of the Profiler. 
5. In the Overview area expand "BehaviourUpdate" then "TestController.Update()". There you can see the results of each test.

## Observations ##
(Based on Unity 5.5.4 using standard .NET 3.5 and Unity 2017.2 using Experimental .NET 4.6)

* for() : Caching the length of the loop improves speed.
* Caching Transform and GameObject and using local versions is much faster than calling .transform or .gameobject. GameObject caching alone is x2 faster. 
* Using static versions of Vector3 and Vector2 are also significantly faster than Vector3.zero, etc.
* Accessor functions (get{}/set{}) are x10 slower than using raw variables.

### Collections ###
* Arrays ([] arrays, not System.Array) are MUCH faster than all other collections, except Contains() on large sets. Use arrays if possible.
* Calling Resize() before adding many items to a collection can make a significant improvement in speed and GC allocations.
* HashSet is slower than Dictionary for add/remove. I'm not sure why since HashSet is really just the keys part of a Dictionary. Odd.

#### Collection speed overview ####

##### Fastest #####

* Remove (values) : array, Stack, Queue, Dictionary, HashSet
* Remove (keys) : Dictionary
* Contains : Dictionary, HashSet, array (for small sets < ~150)
* Add : array, Stack, Queue, List
* Iterate : array, FastList, List[] (List foreach is slower)

##### Slowest #####

* Remove (values) : List, Linked List (allocates in .NET 3.5!, not 4.6)
* Remove (keys) : HashSet (allocates in .NET 3.5!, not 4.6)
* Contains : Queue, Linked List, List (array is much faster), Stack
* Add : Dictionary, HashSet, ArrayList, Linked List
* Iterate : System.Array, Dictionary, HashSet (about x7-10 slower than array), any foreach() in general

## Contacts ##

http://garretpolk.com
Twitter : https://twitter.com/garretpolk  @garretpolk

## License ##
Revised BSD License

Copyright(c) 2018, Garret Polk
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

* Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
* Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
* Neither the name of the Garret Polk nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

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
