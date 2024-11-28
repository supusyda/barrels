using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class HandleSave
{
    // Start is called before the first frame update
    public static readonly string DATA_FOLDER = Application.dataPath + "/Save/";
    public static void Save(string jsonData, bool isOverwrite = false)
    {
        Init();
        string filePath = DATA_FOLDER;
        int saveNumber = 1;
        while (File.Exists(filePath + "save" + saveNumber + ".txt")) // find max save file number
        {
            saveNumber++;
        }
        filePath = DATA_FOLDER + "save" + saveNumber + ".txt";

        File.WriteAllText(filePath, jsonData);
        Debug.Log("SAVE SUCCESS AT " + filePath);
    }
    static void Init()
    {
        if (Directory.Exists(DATA_FOLDER)) return;
        Directory.CreateDirectory(DATA_FOLDER);
        Debug.Log("CREATE SAVE FOLDER");
    }
    public static string Load()
    {

        DirectoryInfo directoryInfo = new DirectoryInfo(DATA_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles("*.txt");
        FileInfo mostRecentSave = null;

        foreach (FileInfo file in saveFiles)
        {
            if (mostRecentSave == null) mostRecentSave = file;
            else
            {
                if (file.LastWriteTime > mostRecentSave.LastWriteTime) mostRecentSave = file;
            }
        }
        if (mostRecentSave == null) return null;
        string saveFile = File.ReadAllText(mostRecentSave.FullName);

        return saveFile;
        // if (File.Exists(DATA_FOLDER + "save1.txt"))
        // {
        //     string saveData = File.ReadAllText(DATA_FOLDER + "save1.txt");
        //     return saveData;
        // }
        // else
        // {
        //     return null;
        // }
    }
    public static string Load(int level)
    {
        if (File.Exists(DATA_FOLDER + "save" + level + ".txt"))
        {
            string saveData = File.ReadAllText(DATA_FOLDER + "save" + level + ".txt");
            return saveData;
        }
        else
        {
            return null;
        }
    }
    public static FileInfo[] GetAllSaveFile()
    {
        DirectoryInfo directoryInfo = new DirectoryInfo(DATA_FOLDER);
        FileInfo[] saveFiles = directoryInfo.GetFiles();
        return saveFiles;
    }
    public static int GetSaveFileAmount()
    {

        return GetAllSaveFile().Length;
    }
    public static bool HasLevel(int level)
    {
        return File.Exists(DATA_FOLDER + "save" + level + ".txt");
    }

}
