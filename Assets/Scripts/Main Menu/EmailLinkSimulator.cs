using UnityEngine;

public class EmailLinkSimulator : MonoBehaviour
{
    public void SimulateEmailLink()
    {
        // Change entry state (this part is correct)
        EntryStateManager.Instance.entryState = EntryStateManager.EntryState.EmailConfirmed;

        // ✔ FIXED: Use DifficultyManager instead of GameManager
        SaveManager.Instance.SavePlayerData(
            GameManager.Instance.spiritPoints,
            DifficultyManager.Instance.currentDifficulty
        );

        Debug.Log("Email link confirmed! YAMI awakened.");
    }
}
