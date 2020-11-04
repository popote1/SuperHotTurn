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
    public TMP_Text ShotFire;
    public TMP_Text MovesMade;
    public GameObject VictroyPannel;
    public JuiceManager JuiceManager;
    
    private static GridActorPlayer _player;
    private static List<GameObject> _ennemies;
    private static TimeManager _timeManager;
    private static bool _isPause;
    private bool _gameEnd;
    private int _moves = 0;
    private int _shotsFire = 0;
    void Start()
    {
        _timeManager = GetComponent<TimeManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")&&!_gameEnd)
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
        _player.PlayerFire.AddListener(CompteFireShot);
        _player.DirectorMove.AddListener(ChangeUIAmmo);
        _player.DirectorMove.AddListener(CompteMoves);
        JuiceManager.SetCamera(_player.GetComponentInChildren<Camera>());
        
    }

    public  void EnnemiKill(GameObject ennemi)
    {
        JuiceManager.EnnemieDie();
        _ennemies.Remove(ennemi);
        ChangeUIEnnemiLeft();
        if (_ennemies.Count<=0)
        {
            Victory();
        }
    }

    public void ChangeUIAmmo()
    {
        BulletLeft.text = _player.AmmoLeft+"";
    }

    private  void  ChangeUIEnnemiLeft()
    {
        EnnemisLeft.text = _ennemies.Count + "";
        
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

    public void CompteFireShot()
    {JuiceManager.Shot();
        _shotsFire++;
    }

    public void CompteMoves()
    {JuiceManager.Move();
        _moves++;
    }

    private void Victory()
    {
        _gameEnd = true;
        _isPause = true;
        VictroyPannel.SetActive(true);
        ShotFire.text = _shotsFire + "";
        MovesMade.text = _moves + "";
    }
}

