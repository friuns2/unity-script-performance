using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Project created by Garret Polk from Longsword Studios, Inc.
/// http://www.longswordstudios.com
/// Code comments are mine.
/// 
/// Code was taken from the slides of Søren Trautner Madsen
/// from the talk/video below. soren@playdead.com
///
/// https://docs.google.com/presentation/d/1dew0TynVmtQf8OMLEz_YtRxK32a_0SAZU9-vgyMRPlA
/// https://www.youtube.com/watch?v=mQ2KTRn4BMI&t
///
/// Performance improvements using a GameObject transform
/// </summary>
public class Test_Transform : MonoBehaviour
{
    // getter/setter versions
    public float gsSpeed { get; set; }
    public float gsSpeedfactor { get; set; }
    public float gsSomeOtherFactor { get; set; }
    public float gsDrag { get; set; }
    public float gsFriction { get; set; }

    // raw public variable versions
    public float speed;
    public float speedfactor;
    public float someOtherFactor;
    public float drag;
    public float friction;

    public Vector3 wantedVelocity;

    // Variable caching
    Transform _transform;
    Vector3 cachedLocalPosition;
    public static float globalDeltaTime;

    void Start()
    {
        _transform = transform;
        cachedLocalPosition = _transform.localPosition;

        // Make sure the character is actually moving.
        // -- Garret Polk
        speed = .01f;
        speedfactor = 1f;
        someOtherFactor = 5f;
        drag = 1f;
        friction = 1f;
        wantedVelocity = Vector3.one;

        gsSpeed = .01f;
        gsSpeedfactor = 1f;
        gsSomeOtherFactor = 5f;
        gsDrag = 1f;
        gsFriction = 1f;

    }

    /// <summary>
    /// This is typical Update() code to move a GameObject.
    /// We can make it faster with some changes.
    /// </summary>
    public void UpdateCharacter()
    {
        Vector3 lastPos = transform.position;
        transform.position = lastPos
                           + wantedVelocity * speed * speedfactor
                           * Mathf.Sin(someOtherFactor)
                           * drag * friction * Time.deltaTime;
    }

    /// <summary>
    /// Move all floating point operations together,
    /// then apply the result ONCE to the Vector of wantedVelocity.
    /// </summary>
    public void UpdateCharacter_ReduceVectorOps()
    {
        Vector3 lastPos = transform.position;
        transform.position = lastPos
                           + wantedVelocity * (speed * speedfactor
                           * Mathf.Sin(someOtherFactor)
                           * drag * friction * Time.deltaTime);
    }

    /// <summary>
    /// Cache the GameObject.transform. Yeah, I thought
    /// Unity takes care of this too, but look at the performance
    /// difference!
    /// </summary>
    public void UpdateCharacter_CachedTransforms()
    {
        Vector3 lastPos = _transform.position; //cached in “void Start()”
        _transform.position = lastPos
                              + wantedVelocity * (speed * speedfactor
                              * Mathf.Sin(someOtherFactor)
                              * drag * friction * Time.deltaTime);
    }

    /// <summary>
    /// Use tranform.localPosition instead of the world position
    /// of transform.position.
    /// </summary>
    public void UpdateCharacter_LocalPosition()
    {
        Vector3 lastPos = _transform.localPosition;
        _transform.localPosition = lastPos
                             + wantedVelocity * (speed * speedfactor
                             * Mathf.Sin(someOtherFactor)
                             * drag * friction * Time.deltaTime);
    }

    /// <summary>
    /// Cache the local position. Note : you will have
    /// to insure that other code doesn't directly modify
    /// the localPosition or it will get out of sync.
    /// </summary>
    public void UpdateCharacter_ReduceEngineCalls()
    {
        cachedLocalPosition += wantedVelocity * (speed * speedfactor
                             * Mathf.Sin(someOtherFactor)
                             * drag * friction * Time.deltaTime);
        _transform.localPosition = cachedLocalPosition;
    }

    /// <summary>
    /// Set the individual axis values ourselves. 
    /// </summary>
    public void UpdateCharacter_NoVectorMath()
    {
        float factor = speed * speedfactor
                     * Mathf.Sin(someOtherFactor)
                     * drag * friction * Time.deltaTime;

        cachedLocalPosition.x += wantedVelocity.x * factor;
        cachedLocalPosition.y += wantedVelocity.y * factor;
        cachedLocalPosition.z += wantedVelocity.z * factor;
        _transform.localPosition = cachedLocalPosition;
    }

    /// <summary>
    /// This shows the impact of get/set instead of 
    /// raw public variables.
    /// </summary>
    public void UpdateCharacter_CacheDeltaTimeGetSet()
    {
        float factor = gsSpeed * gsSpeedfactor
                     * Mathf.Sin(gsSomeOtherFactor)
                     * gsDrag * gsFriction * globalDeltaTime;

        cachedLocalPosition.x += wantedVelocity.x * factor;
        cachedLocalPosition.y += wantedVelocity.y * factor;
        cachedLocalPosition.z += wantedVelocity.z * factor;
        _transform.localPosition = cachedLocalPosition;
    }

    /// <summary>
    /// ** Fastest code, best example **
    ///
    /// Avoid calling Time.deltaTime more than once.
    /// We cache it and set it in the Controller code.
    /// </summary>
    public void UpdateCharacter_CacheDeltaTime()
    {
        float factor = speed * speedfactor
                     * Mathf.Sin(someOtherFactor)
                     * drag * friction * globalDeltaTime;

        cachedLocalPosition.x += wantedVelocity.x * factor;
        cachedLocalPosition.y += wantedVelocity.y * factor;
        cachedLocalPosition.z += wantedVelocity.z * factor;
        _transform.localPosition = cachedLocalPosition;
    }
}
