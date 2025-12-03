using UnityEngine;

public class MenuGlitchController : MonoBehaviour
{
    public GameObject normalMenuGraphic;
    public GameObject corruptedMenuGraphic;

    void Start()
    {
        var state = EntryStateManager.Instance.entryState;

        if (state == EntryStateManager.EntryState.EmailConfirmed)
        {
            normalMenuGraphic.SetActive(false);
            corruptedMenuGraphic.SetActive(true);
        }
        else
        {
            normalMenuGraphic.SetActive(true);
            corruptedMenuGraphic.SetActive(false);
        }
    }
}
