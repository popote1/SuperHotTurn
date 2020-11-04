using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuiceManager : MonoBehaviour
{
    public ScreenShake ScreenShake;
    public GameObject AudioObject;
    [Header("sounds")]
    public AudioClip ShotSound;
    public AudioClip MoveSound;
    public AudioClip DieSound;
    [Header("sounds settings")] 
    [Range(0,1)]public float volume;
    
    public void Shot()
    {
        ScreenShake.StartShake();
        Play(ShotSound);
        
    }

    public void Move()
    {
        Play(MoveSound);
    }

    public void EnnemieDie()
    {
        Play(DieSound);
        ScreenShake.StartShake();
    }

    private void Play(AudioClip sound)
    {
        AudioSource ausu =AudioObject.AddComponent<AudioSource>();
        ausu.clip = sound;
        ausu.volume = volume;
        ausu.Play();
        Destroy(ausu,sound.length);

    }

    public void SetCamera(Camera cam)
    {
        ScreenShake.Camera = cam.gameObject;
    }
}
