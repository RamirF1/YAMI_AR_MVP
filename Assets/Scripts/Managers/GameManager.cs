using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int spiritPoints = 0;

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
    }

    public void SetDifficulty(int difficultyIndex)
    {
        DifficultyManager.Instance.SetDifficulty(difficultyIndex);
    }

    public void ApplyCurrentDifficulty()
    {
        // CONVERT enum -> int
        DifficultyManager.Instance.SetDifficulty(
            (int)DifficultyManager.Instance.currentDifficulty
        );
    }

    public void AddSpiritPoints(int amount)
    {
        spiritPoints += amount;
        UIManager.Instance.UpdateSpiritPoints(spiritPoints);
    }

    public void StartGame()
    {
        ApplyCurrentDifficulty();
        SceneManager.LoadScene("AR_Main");
    }
}