using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class DataHandler 
{
    /// <summary>
    /// this Method allow to save data in the specified unityDirectoty
    /// </summary>
    /// <param name="unityFolder">Selected Unity Directory</param>
    /// <param name="data"> the serializable Data</param>
    /// <param name="fileName">The Data FilName</param>
    /// <typeparam name="T">The Serializable data type</typeparam>
    public static void Save<T>(UnityFolder unityFolder, T data,String fileName)
    {
        
        CheckAndCreatDirectory(unityFolder);
        string filePhath = GetDirectory(unityFolder) + Path.AltDirectorySeparatorChar + fileName;
        //Transform data to readable Json
        string JsonData = JsonUtility.ToJson(data);
        
        
        //open the file path with de right access
        FileStream fileStream = new FileStream(filePhath, FileMode.Create);
        //open the string righter on the filestream
        StreamWriter streamWriter = new StreamWriter(fileStream);
        //Write the Json Data using the stramWriter
        streamWriter.Write(JsonData);
        //Close the streamWriter
        streamWriter.Close();
        //Close the filesStream
        fileStream.Close();
        

    }

    public static T Load<T>(UnityFolder unityFolder, string fileName)
    {
        CheckAndCreatDirectory(unityFolder);
        string filePhath = GetDirectory(unityFolder) + Path.AltDirectorySeparatorChar + fileName;
        //Check if file existe
        if (!CheckFile(filePhath))
        {
            throw new Exception("Pas de fichier de save");
        }
        //open the stream on the file
        StreamReader sr = new StreamReader(filePhath);
        // read the file into string
        string jsonData = sr.ReadToEnd();
        //Close the streamReader
        sr.Close();
        //transform Json string into data
        return JsonUtility.FromJson<T>(jsonData);
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
}
public enum UnityFolder
{
    stremingAsset,PersitentData,DataPath,TemporaryCache
}