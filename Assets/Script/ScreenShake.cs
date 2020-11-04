using System.Collections;
using System.Resources;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    public float duration;
    public float frequance;
    public float magnitude;
    public GameObject Camera;

    private bool _doShake;
    private float _totaltimer;
    private float _frenceTimer;
    private Vector3 _originalPos;
    private float x;
    private float y;
    private float z;
    // Update is called once per frame
    void Update()
    {
        
        if (_doShake)
        {
            if (_totaltimer < duration)
            {
                if (_frenceTimer > frequance)
                {
                    float x = Random.Range(-1, 1) * magnitude;
                    float y = Random.Range(-1, 1) * magnitude;
                    float z = Random.Range(-1, 1) * magnitude;
                    Camera.transform.position = new Vector3(x, y, z)+_originalPos;
                    _frenceTimer = 0;
                }
                else
                {
                    _frenceTimer += Time.deltaTime;
                }

                _totaltimer += Time.deltaTime;
            }
            else
            {
                _doShake = false;
                Camera.transform.position = _originalPos;
            }
        }
    }

    public void StartShake()
    {
        _originalPos = Camera.transform.position;
        _totaltimer = 0;
        _frenceTimer = 0;
        _doShake = true;
    }

   
}
