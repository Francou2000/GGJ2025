using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/savedata.json";

    public static void SaveGame(int lives, float time, Vector3 position)
    {
        SaveData data = new SaveData
        {
            lives = lives,
            time = time,
            positionX = position.x,
            positionY = position.y,
            positionZ = position.z
        };

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(saveFilePath, json);
        Debug.Log("Data saved: " + saveFilePath);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);

            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Data loaded from: " + saveFilePath);
            return data;
        }
        else return null;
    }

    public static void DeleteAllData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Data deleted");
        }
    }
}