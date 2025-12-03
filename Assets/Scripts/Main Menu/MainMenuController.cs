using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject normalMenuPanel;      // “YAMI + New Game”
    public GameObject corruptedMenuPanel;   // optional corrupted menu
    public GameObject emailPanel;           // Email input panel
    public GameObject newsPopupPanel;       // After email submit
    public GameObject eventPanel;           // After news popup
    public GameObject difficultyPanel;      // Difficulty selection

    [Header("Controllers")]
    public EmailRequestController emailRequest;   // MUST be assigned

    private void Start()
    {
        var state = EntryStateManager.Instance.entryState;

        // First launch, or email not confirmed → show normal menu only
        if (state == EntryStateManager.EntryState.FirstLaunch ||
            state == EntryStateManager.EntryState.EmailRequested)
        {
            ShowInitialMenuState();
            return;
        }

        // Email confirmed → show normal menu (you can switch to corrupted)
        if (state == EntryStateManager.EntryState.EmailConfirmed)
        {
            ShowInitialMenuState();
        }
    }

    private void ShowInitialMenuState()
    {
        normalMenuPanel.SetActive(true);
        corruptedMenuPanel.SetActive(false);

        emailPanel.SetActive(false);
        newsPopupPanel.SetActive(false);
        eventPanel.SetActive(false);
        difficultyPanel.SetActive(false);
    }

    // ------------------------------------------------------------
    // START BUTTON
    // ------------------------------------------------------------
    public void OnStartPressed()
    {
        var state = EntryStateManager.Instance.entryState;

        // First launch → open email input panel
        if (state == EntryStateManager.EntryState.FirstLaunch ||
            state == EntryStateManager.EntryState.EmailRequested)
        {
            // FIX: Ensure this NEVER fails
            if (emailRequest != null)
            {
                emailRequest.OnPressNewGame();
            }
            else
            {
                Debug.LogError("EmailRequestController is NOT assigned in MainMenuController!");
            }
            return;
        }

        // Email already confirmed → skip to news popup
        if (state == EntryStateManager.EntryState.EmailConfirmed)
        {
            normalMenuPanel.SetActive(false);

            newsPopupPanel.SetActive(true);
            eventPanel.SetActive(false);
            difficultyPanel.SetActive(false);
        }
    }

    // ------------------------------------------------------------
    // NEWS POPUP → EVENT PANEL
    // ------------------------------------------------------------
    public void OnNewsPopupContinue()
    {
        newsPopupPanel.SetActive(false);
        eventPanel.SetActive(true);
    }

    // ------------------------------------------------------------
    // EVENT PANEL → DIFFICULTY PANEL
    // ------------------------------------------------------------
    public void OnEventContinue()
    {
        eventPanel.SetActive(false);
        difficultyPanel.SetActive(true);
    }

    // ------------------------------------------------------------
    // DIFFICULTY BUTTONS → LOAD GAME
    // ------------------------------------------------------------
    public void PlaySane()
    {
        DifficultySelection.chosenDifficulty = DifficultyManager.Difficulty.Sane;
        DifficultyManager.Instance.SetDifficulty(DifficultyManager.Difficulty.Sane);
        SceneManager.LoadScene("AR_Main");
    }

    public void PlayInsane()
    {
        DifficultySelection.chosenDifficulty = DifficultyManager.Difficulty.Insane;
        DifficultyManager.Instance.SetDifficulty(DifficultyManager.Difficulty.Insane);
        SceneManager.LoadScene("AR_Main");
    }

    public void PlayMadhouse()
    {
        DifficultySelection.chosenDifficulty = DifficultyManager.Difficulty.Madhouse;
        DifficultyManager.Instance.SetDifficulty(DifficultyManager.Difficulty.Madhouse);
        SceneManager.LoadScene("AR_Main");
    }
}
