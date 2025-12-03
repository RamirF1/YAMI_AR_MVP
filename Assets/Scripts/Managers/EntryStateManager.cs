using UnityEngine;

public class EntryStateManager : MonoBehaviour
{
    public static EntryStateManager Instance;

    public enum EntryState
    {
        FirstLaunch,      // App opened for the first time
        EmailPanelOpen,   // Player pressed Start, email field is open
        EmailRequested,   // Player submitted email → "Check your email"
        EmailConfirmed    // Player clicked email link → corrupted menu
    }

    public EntryState entryState = EntryState.FirstLaunch;

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
}
