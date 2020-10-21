using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public  class TimeManager : MonoBehaviour
{
    public GameObject Director;
    public static UnityEvent TTStart = new UnityEvent();
    public static UnityEvent TTUpdate= new UnityEvent();
    public static UnityEvent TTLateUpdate= new UnityEvent();

    private void Start()
    {
        
    }

    public static void DirectorMove()
    {
       // Debug.Log("DirectorMove");
       TTStart.Invoke();
       TTUpdate.Invoke();
       TTLateUpdate.Invoke();
    }
    
    
}
