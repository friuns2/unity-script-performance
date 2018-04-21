using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Project created by Garret Polk
/// http://www.garretpolk.com
/// Code comments are mine.
/// 
/// Tests speed of caching the GameObject for a MonoBehaviour
/// </summary>
public class Test_GameObject : MonoBehaviour
{
    public GameObject _go;

    void Awake()
    {
        _go = gameObject;
    }

    // Test doing the least intrusive thing I can think of.
    public void Update_GameObject()
    {
        GameObject goTest = gameObject;
        //gameObject.SetActive(true);
    }

    // Test doing the least intrusive thing I can think of.
    public void Update_CachedGameObject()
    {
        GameObject goTest = _go;
        //_go.SetActive(true);
    }
}
