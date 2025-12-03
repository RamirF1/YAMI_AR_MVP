using UnityEngine;

public class GhostBehavior : MonoBehaviour
{
    [Header("Stats")]
    public float ghostSpeed;
    public float flickerChance;
    public float captureWindow;

    private bool difficultyApplied = false;

    void Start()
    {
        ApplyDifficultySettings();
    }

    public void ApplyDifficultySettings()
    {
        if (difficultyApplied) return;

        // FIXED: access difficulty through the Instance
        var diff = DifficultyManager.Instance.currentDifficulty;

        switch (diff)
        {
            case DifficultyManager.Difficulty.Sane:
                ghostSpeed = 0.4f;
                flickerChance = 0.05f;
                captureWindow = 0.45f;
                break;

            case DifficultyManager.Difficulty.Insane:
                ghostSpeed = 1.2f;
                flickerChance = 0.25f;
                captureWindow = 0.20f;
                break;

            case DifficultyManager.Difficulty.Madhouse:
                ghostSpeed = 2.0f;
                flickerChance = 0.35f;
                captureWindow = 0.15f;
                break;
        }

        difficultyApplied = true;
        Debug.Log("Ghost difficulty applied: " + diff);
    }

    void Update()
    {
        // Example movement for testing
        transform.Translate(Vector3.forward * ghostSpeed * Time.deltaTime);
    }
}
