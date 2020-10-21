using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatButton : MonoBehaviour
{
    public Transform parent;
    public GameObject buton;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Click()
    {
        Instantiate(buton, parent);
    }
}
