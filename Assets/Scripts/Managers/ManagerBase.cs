using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//All classes that are singletons can derive from "ManagerBase<T>" where T is replaced with the class name
public class ManagerBase<T> : MonoBehaviour where T : Component
{

    public static T x;

    private void Awake()
    {
        if (x == null) x = FindObjectOfType<T>();
        else Destroy(gameObject);
    }

    protected virtual void Init()
    {
        
    }

}
