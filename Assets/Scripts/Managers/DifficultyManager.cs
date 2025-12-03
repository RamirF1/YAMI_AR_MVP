using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager Instance;

    public enum Difficulty
    {
        Sane,
        Insane,
        Madhouse
    }

    [Header("Current Difficulty")]
    public Difficulty difficulty = Difficulty.Sane;

    // ⭐ FIX 1 — Other scripts expect "currentDifficulty", so we add it
    public Difficulty currentDifficulty
    {
        get { return difficulty; }
        set { difficulty = value; }
    }

    [Header("Sane Settings")]
    public float saneGhostSpeed = 0.4f;
    public float saneFlickerChance = 0.05f;
    public float saneCaptureWindow = 1.0f;

    [Header("Insane Settings")]
    public float insaneGhostSpeed = 0.8f;
    public float insaneFlickerChance = 0.15f;
    public float insaneCaptureWindow = 0.7f;

    [Header("Madhouse Settings")]
    public float madhouseGhostSpeed = 1.2f;
    public float madhouseFlickerChance = 0.30f;
    public float madhouseCaptureWindow = 0.4f;

    [HideInInspector] public float ghostSpeed;
    [HideInInspector] public float flickerChance;
    [HideInInspector] public float captureWindow;

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

    private void Start()
    {
        ApplyDifficultySettings();
    }

    public void SetDifficulty(Difficulty newDiff)
    {
        difficulty = newDiff;
        ApplyDifficultySettings();
    }

    // ⭐ FIX 2 — Your GameManager sends an INT, so we add support for int → Difficulty
    public void SetDifficulty(int index)
    {
        if (index < 0 || index > 2)
            index = 0; // fallback to Sane

        difficulty = (Difficulty)index;
        ApplyDifficultySettings();
    }

    public void ApplyDifficultySettings()
    {
        switch (difficulty)
        {
            case Difficulty.Sane:
                ghostSpeed = saneGhostSpeed;
                flickerChance = saneFlickerChance;
                captureWindow = saneCaptureWindow;
                break;

            case Difficulty.Insane:
                ghostSpeed = insaneGhostSpeed;
                flickerChance = insaneFlickerChance;
                captureWindow = insaneCaptureWindow;
                break;

            case Difficulty.Madhouse:
                ghostSpeed = madhouseGhostSpeed;
                flickerChance = madhouseFlickerChance;
                captureWindow = madhouseCaptureWindow;
                break;
        }
    }
}
