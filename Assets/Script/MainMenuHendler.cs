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
    [Header("EditePannel")]
    public GameObject PannelEdit;
    public TMP_InputField EditeName;
    public TMP_InputField EditHauter;
    public TMP_InputField EditLargeur;

    [HideInInspector] public PlayGrid PlayGrid;
    [HideInInspector] public string NewName;
    [HideInInspector] public int NewHauteur;
    [HideInInspector] public int NewLargeur;
    

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
        PannelEdit.SetActive(true);
    }

    public void UIEditePannelReturn()
    {
        PannelEdit.SetActive(false);
    }

    public void UIEditeCeate()
    {
        NewName = EditeName.text;
        NewHauteur = int.Parse(EditHauter.text);
        NewLargeur = int.Parse(EditLargeur.text);
        if (NewHauteur <= 0)
        {
            if (NewHauteur == 0) NewHauteur = 1;
            else NewHauteur = NewHauteur * -1;
        }

        if (NewLargeur <= 0)
        {
            if (NewLargeur == 0) NewLargeur = 1;
            else NewLargeur = NewLargeur * -1;
        }
        

        SceneManager.LoadScene(2);
    }
    
    public void UIQuite()
    {
        Application.Quit();
    }
}
