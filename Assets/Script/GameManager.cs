using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI")] 
    public  TMP_Text BulletLeft;
    public  TMP_Text EnnemisLeft;
    public   GameObject PannelPause;
    
    private static GridActorPlayer _player;
    private static List<GameObject> _ennemies;
    private static TimeManager _timeManager;
    private static bool _isPause;
    void Start()
    {
        _timeManager = GetComponent<TimeManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (_isPause)
            {
                UIReprendre();
            }
            else
            {
                TimeManager.TTSetPause.Invoke();
                _isPause = true;
                PannelPause.SetActive(true);

            }
        }
        
    }
    public  void SetUpGame(List<GameObject> ennemis,GameObject player)
    {
        _ennemies = ennemis;
        EnnemisLeft.text = _ennemies.Count + "";
        _player = player.GetComponent<GridActorPlayer>();
        _player.PlayerFire.AddListener(ChangeUIAmmo);
        _player.DirectorMove.AddListener(ChangeUIAmmo);
        
    }

    public void ChangeUIAmmo()
    {
        BulletLeft.text = _player.AmmoLeft+"";
    }


    public void UIReprendre()
    {
        PannelPause.SetActive(false);
        TimeManager.TTSetPause.Invoke();
        _isPause = false;
    }

    public void UIMaineMen()
    {
        SceneManager.LoadScene(0);
    }

    public void UIQuite()
    {
        Application.Quit();
    }
}

