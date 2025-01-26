using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/savedata.json";
    private static string levelFilePath = Application.persistentDataPath + "/leveldata.json";

    public static void SaveGame(int lives, float time, Vector3 position)
    {
        SaveData data = new SaveData
        {
            lives = lives,
            time = time,
            positionX = position.x,
            positionY = position.y,
            positionZ = position.z,
        };

        string json = JsonUtility.ToJson(data, true);

        File.WriteAllText(saveFilePath, json);
        Debug.Log("Data saved: " + saveFilePath);
    }

    public static void SaveLevel(bool chickenMode)
    {
        LevelData levelData = new LevelData
        {
            chickenMode = chickenMode
        };

        string json = JsonUtility.ToJson(levelData, true);

        File.WriteAllText(levelFilePath, json);
        Debug.Log("Level data saved: " + levelFilePath);
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

    public static LevelData LoadLevel()
    {
        if (File.Exists(levelFilePath))
        {
            string json = File.ReadAllText(levelFilePath);

            LevelData levelData = JsonUtility.FromJson<LevelData>(json);
            Debug.Log("Level data loaded from: " + levelFilePath);
            return levelData;
        }
        else return null;
    }

    public static void DeleteAllData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
            Debug.Log("Game data deleted");
        }
        if (File.Exists(levelFilePath))
        {
            File.Delete(levelFilePath);
            Debug.Log("Level data deleted");
        }
    }
}