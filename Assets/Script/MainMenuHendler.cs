using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class MainMenuHendler : MonoBehaviour
{

    public InputField LevelName;
    public GameObject BoutonSave;
    public GameObject PannelDesSaves;
    public GameObject PannelLoad;
    
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
           if (!Path.HasExtension(save))
           {
               GameObject bouton = Instantiate(BoutonSave, PannelDesSaves.transform);
               bouton.GetComponentInChildren<TMP_Text>().text = Path.GetFileName(save);
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
