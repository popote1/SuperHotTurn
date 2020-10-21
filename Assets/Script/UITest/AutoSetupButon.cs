using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutoSetupButon : MonoBehaviour
{
    private void Awake()
    {
        foreach (Transform child in transform.parent)
        {
            Debug.Log(child.name);
        }
    }
}
