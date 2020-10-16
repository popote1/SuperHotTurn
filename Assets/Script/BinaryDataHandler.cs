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
        
        FileStream fs = new FileStream(filePath,FileMode.OpenOrCreate);
        BinaryFormatter formatter =new BinaryFormatter();
        formatter.Serialize(fs ,data);
        fs.Close();
    }

    public static T Load<T>(UnityFolder unityFolder, string fileName)
    {
        CheckAndCreatDirectory(unityFolder);
        string filePath = GetDirectory(unityFolder) + Path.AltDirectorySeparatorChar + fileName;
        
        if (!CheckFile(filePath))
        {
            throw new Exception("Pas de fichier de save à "+filePath);
        }
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);
        T data = (T) bf.Deserialize(fs);
        return data;
    }

    public static string[] CheckForFiles(UnityFolder unityFolder)
    {
        return Directory.GetFiles(GetDirectory(unityFolder) + Path.AltDirectorySeparatorChar);
        
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

