using UnityEngine;

public class RitualOverlayController : MonoBehaviour
{
    public GameObject ritualOverlay;
    public TMPro.TextMeshProUGUI ritualText;

    void Start()
    {
        if (EntryStateManager.Instance.entryState == EntryStateManager.EntryState.EmailConfirmed)
        {
            ritualOverlay.SetActive(true);
            ritualText.text = "Ritual incomplete.";
        }
        else
        {
            ritualOverlay.SetActive(false);
        }
    }
}
