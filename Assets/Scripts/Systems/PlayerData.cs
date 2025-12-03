[System.Serializable]
public class PlayerData
{
    public int points;
    public DifficultyManager.Difficulty difficulty;

    public EntryStateManager.EntryState entryState;

    public string email;   // NEW — stores the player's email
}
