using UnityEngine;
using TMPro;

public class EmailRequestController : MonoBehaviour
{
    [Header("Panels")]
    public GameObject emailPanel;
    public GameObject newsPopupPanel;
    public GameObject normalMenuPanel;
    public GameObject eventPanel;
    public GameObject difficultyPanel;

    [Header("UI")]
    public TMP_InputField emailInputField;
    public TextMeshProUGUI emailLabel;

    public void OnPressNewGame()
    {
        normalMenuPanel.SetActive(false);
        emailPanel.SetActive(true);
    }

    public void OnSubmitEmail()
    {
        string email = emailInputField.text.Trim();

        if (string.IsNullOrEmpty(email))
        {
            emailLabel.text = "Please enter a valid email.";
            return;
        }

        EntryStateManager.Instance.entryState = EntryStateManager.EntryState.EmailConfirmed;

        emailPanel.SetActive(false);

        newsPopupPanel.SetActive(true);
        eventPanel.SetActive(false);
        difficultyPanel.SetActive(false);
    }
}
