# Unity Script Performance Examples #

This project is a set of examples and utilities to help you improve Unity's C# script performance.

### FEATURES ###

* GameObject.transform improvements
* Faster string comparison functions - StartsWith(), etc.
* Faster access to common static data like Vector3.zero
* Prevent garbage collection memory allocations using Enum as a key in containers

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

### USE ###

https://docs.unity3d.com/Manual/ProfilerCPU.html

1. In Unity select Window > Profiler
2. Select "Deep Profile" button at the top
* Each test int the scene is in a GameObject under "Performance Tests" and can be turned on/off individually by enable/disabling the GameObject.
3. Play the "game".
4. After a couple seconds pause the game and click in the the CPU Usage area of the Profiler. 
5. In the Overview expand "BehaviourUpdate", there you can see the results of each test.

### Contacts ###

http://longswordstudios.com
Twitter : https://twitter.com/garretpolk  @garretpolk