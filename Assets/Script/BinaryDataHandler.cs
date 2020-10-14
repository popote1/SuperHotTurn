using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;

public static class BinaryDataHandler 
{
    public static void Save<T>(UnityFolder unityFolder,T data,string fileName)
    {
        CheckAndCreatDirectory(unityFolder);

        string filePath = GetDirectory(unityFolder) + Path.AltDirectorySeparatorChar + fileName;
        
        FileStream fs = new FileStream(filePath,FileMode.Create);
        BinaryFormatter formatter =new BinaryFormatter();
        formatter.Serialize(fs ,data);
        fs.Close();
    }
    
    
    private static void CheckAndCreatDirectory(UnityFolder unityFolder)
    {
        if (!File.Exists(GetDirectory(unityFolder))) Directory.CreateDirectory((GetDirectory(unityFolder)));
    }

    private static bool CheckFile(string filePath)
    {
        bool isFilerExist = File.Exists(filePath);
        return isFilerExist;
    }

    private static string GetDirectory(UnityFolder unityFolder)
    {
        switch (unityFolder)
        {
            case UnityFolder.stremingAsset:
                return Application.streamingAssetsPath;
            case UnityFolder.PersitentData:
                return Application.persistentDataPath;
            case UnityFolder.DataPath :
                return Application.dataPath;
            case UnityFolder.TemporaryCache:    
                return Application.temporaryCachePath;
            default:
                throw new ArgumentOutOfRangeException(nameof(unityFolder), unityFolder, null);
        }
    }
    public enum UnityFolder
    {
        stremingAsset,PersitentData,DataPath,TemporaryCache
    }
}

