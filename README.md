# Unity Script Performance Examples #

This project is a set of examples and utilities to help you improve Unity's C# script performance.
https://bitbucket.org/GarretPolk/unity-script-performance

## FEATURES ##

* GameObject.transform improvements
* Faster string comparison functions - StartsWith(), etc.
* Faster access to common static data like Vector3.zero
* Prevent garbage collection memory allocations using Enum as a key in containers
* Speed tests for collection types (List, Dictionary, etc.) for Contains/Add/Remove().

### Details ###

Project created by Garret Polk from Longsword Studios, Inc. 
http://www.longswordstudios.com

### Videos and slides ###
 
Transform example code was taken from the slides of Søren Trautner Madsen from the talk/video below. soren@playdead.com

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

Caching Transform and GameObject and using local versions is much faster than calling .transform or .gameobject. GameObject caching alone is x2 faster. 

Using static versions of Vector3 and Vector2 are also significantly faster than Vector3.zero, etc.

Accessor functions (get{}/set{}) are x10 slower than using raw variables.

### Collections ###
Arrays are MUCH faster than all other collections, except Contains() on large sets. Use arrays if possible.
Calling Resize() before adding many items to a collection can make a significant improvement in speed and GC allocations.
HashSet is slower than Dictionary. I'm not sure why since HashSet is really just the keys part of a Dictionary. Odd.

Collection speed overview
Fastest:
Remove (values) : Array, Stack, Queue, Dictionary, HashSet
Remove (keys) : Dictionary
Contains : Dictionary, HashSet
Add : Array, Stack, Queue, List

Slow:
Remove (values) : List, Linked List (allocates!)
Remove (keys) : HashSet (allocates!)
Contains : Queue, Linked List, List (array is much faster), Stack
Add : Dictionary, HashSet, ArrayList, Linked List

## Contacts ##

http://longswordstudios.com
Twitter : https://twitter.com/garretpolk  @garretpolk