using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveLoadManager
{
    public static void SaveData(SphereStats sphereData)
    {
        BinaryFormatter formatter = new();
        string path = Application.persistentDataPath + "/Sphere.dat";

        FileStream stream = new(path, FileMode.Create);

        SaveLoadClass playerData = new(sphereData);

        formatter.Serialize(stream,playerData);
        stream.Close();

    }

    public static SaveLoadClass LoadData()
    {
        string path = Application.persistentDataPath + "/Sphere.dat";

        if(File.Exists(path))
        {
            BinaryFormatter formatter = new();
            FileStream stream = new(path, FileMode.Open);

            SaveLoadClass playerData = formatter.Deserialize(stream) as SaveLoadClass;

            stream.Close();

            return playerData;
        }
        else
        {
            Debug.LogWarning($"Save file not found! Path: {path}");
            return null;
        }
    }
}
