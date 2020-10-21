using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class MainMenuHendler : MonoBehaviour
{

    public InputField LevelName;
    public GameObject BoutonSave;
    public GameObject PannelDesSaves;
    public GameObject PannelLoad;

    [HideInInspector] public PlayGrid PlayGrid;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void UIToPlayMode()
    {
        PannelLoad.SetActive(true);
        //List<GameObject> oldSaves = new List<GameObject>();
        foreach (Transform child in PannelDesSaves.transform)
        {Destroy(child.gameObject, 0.01f);
        }
        string[] saves= BinaryDataHandler.CheckForFiles(BinaryDataHandler.UnityFolder.stremingAsset);
       foreach (var save in saves)
       {
           if (Path.GetExtension(save)==".map")
           {
               GameObject bouton = Instantiate(BoutonSave, PannelDesSaves.transform);
               bouton.GetComponentInChildren<TMP_Text>().text = Path.GetFileNameWithoutExtension(save);
               bouton.GetComponent<Button>().onClick.AddListener(delegate
               {
                   PlayGrid = BinaryDataHandler.Load<PlayGrid>(BinaryDataHandler.UnityFolder.stremingAsset,Path.GetFileNameWithoutExtension(save),BinaryDataHandler.DataFileExtention.map);
                   SceneManager.LoadScene(1);
               });
           }
       }
    }

    public void UIClosePannelLoad()
    {
        PannelLoad.SetActive(false);
    }
    public void UIGoTOEditMode()
    {
        
    }
    public void UIQuite()
    {
        Application.Quit();
    }
}
