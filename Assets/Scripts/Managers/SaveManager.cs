using UnityEngine;
using System.IO;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance;

    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        filePath = Application.persistentDataPath + "/playerData.json";
    }

    public void SavePlayerData(int spiritPoints, DifficultyManager.Difficulty difficulty)
    {
        PlayerData data = new PlayerData();
        data.points = spiritPoints;
        data.difficulty = difficulty;

        // NEW → Save onboarding state
        data.entryState = EntryStateManager.Instance.entryState;

        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(filePath, json);

        Debug.Log("Saved Player Data → " + filePath);
    }

    public PlayerData LoadPlayerData()
    {
        if (!File.Exists(filePath))
        {
            Debug.Log("No save file found.");
            return null;
        }

        string json = File.ReadAllText(filePath);
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);

        // NEW → Load onboarding state
        EntryStateManager.Instance.entryState = data.entryState;

        Debug.Log("Loaded Player Data.");
        return data;
    }
}
