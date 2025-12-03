using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("HUD")]
    public TextMeshProUGUI spiritPointsText;

    [Header("Capture Feedback")]
    public CanvasGroup captureFeedback;
    public float feedbackFadeTime = 0.4f;
    public float feedbackDuration = 1f;

    [Header("Lore Panel")]
    public CanvasGroup lorePanel;
    public TextMeshProUGUI loreText;
    public float loreFadeTime = 0.4f;
    public float loreDisplayTime = 2.2f;   // auto hide time

    private bool loreIsShowing = false;

    private void Awake()
    {
        Instance = this;

        // Ensure panels start hidden
        if (lorePanel != null)
            lorePanel.alpha = 0;

        if (captureFeedback != null)
            captureFeedback.alpha = 0;
    }

    private void Start()
    {
        UpdateSpiritPoints(GameManager.Instance.spiritPoints);
    }

    // ----------------------------------------------------
    // SPIRIT POINTS UPDATE
    // ----------------------------------------------------
    public void UpdateSpiritPoints(int amount)
    {
        if (spiritPointsText != null)
            spiritPointsText.text = "SP: " + amount;
    }

    // ----------------------------------------------------
    // CAPTURE FEEDBACK
    // ----------------------------------------------------
    public void ShowCaptureFeedback()
    {
        StopCoroutine(nameof(FadeOutCaptureFeedback));
        StopCoroutine(nameof(FadeInCaptureFeedback));

        StartCoroutine(FadeInCaptureFeedback());
    }

    private System.Collections.IEnumerator FadeInCaptureFeedback()
    {
        float t = 0;

        while (t < feedbackFadeTime)
        {
            t += Time.deltaTime;
            captureFeedback.alpha = Mathf.Lerp(0, 1, t / feedbackFadeTime);
            yield return null;
        }

        yield return new WaitForSeconds(feedbackDuration);

        StartCoroutine(FadeOutCaptureFeedback());
    }

    private System.Collections.IEnumerator FadeOutCaptureFeedback()
    {
        float t = 0;

        while (t < feedbackFadeTime)
        {
            t += Time.deltaTime;
            captureFeedback.alpha = Mathf.Lerp(1, 0, t / feedbackFadeTime);
            yield return null;
        }
    }

    // ----------------------------------------------------
    // LORE PANEL (MESSAGE POPUPS)
    // ----------------------------------------------------
    public void ShowLore(string text)
    {
        loreText.text = text;

        if (!loreIsShowing)
        {
            loreIsShowing = true;
            StopCoroutine(nameof(FadeOutLore));
            StopCoroutine(nameof(FadeInLore));
            StartCoroutine(FadeInLore());
        }
        else
        {
            // If lore is already visible, refresh the auto-hide timer
            StopCoroutine(nameof(FadeOutLore));
            StartCoroutine(FadeOutLore());  // restart fade out logic
        }
    }

    public void HideLore()
    {
        StopCoroutine(nameof(FadeOutLore));
        StartCoroutine(FadeOutLore());
    }

    private System.Collections.IEnumerator FadeInLore()
    {
        float t = 0;

        while (t < loreFadeTime)
        {
            t += Time.deltaTime;
            lorePanel.alpha = Mathf.Lerp(0, 1, t / loreFadeTime);
            yield return null;
        }

        // Stay visible, then fade out automatically
        yield return new WaitForSeconds(loreDisplayTime);
        StartCoroutine(FadeOutLore());
    }

    private System.Collections.IEnumerator FadeOutLore()
    {
        float t = 0;

        while (t < loreFadeTime)
        {
            t += Time.deltaTime;
            lorePanel.alpha = Mathf.Lerp(1, 0, t / loreFadeTime);
            yield return null;
        }

        loreIsShowing = false;
    }
}
