using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasseBidon : MonoBehaviour
{

    public Bidon Bidon;

    [ContextMenu("Save")]
    public void Save()
    {
        DataHandler.Save(UnityFolder.stremingAsset , Bidon,"savetest");
    }
    
}
[Serializable]
public class Bidon
{
    public string testSave;

   
}
