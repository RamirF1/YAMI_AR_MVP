using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class YamiHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI tmp;

    // Colors
    private Color normalColor = new Color32(229, 243, 253, 255);   // Cotton Boll
    private Color hoverColor = new Color32(255, 255, 255, 255);    // Pure white

    // Scale
    private Vector3 normalScale = Vector3.one;
    private Vector3 hoverScale = new Vector3(1.02f, 1.02f, 1.02f);

    // Speed
    private float speed = 0.15f;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.color = normalColor;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(hoverColor, hoverScale));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTo(normalColor, normalScale));
    }

    private System.Collections.IEnumerator FadeTo(Color targetColor, Vector3 targetScale)
    {
        Color startColor = tmp.color;
        Vector3 startScale = transform.localScale;

        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime / speed;

            tmp.color = Color.Lerp(startColor, targetColor, t);
            transform.localScale = Vector3.Lerp(startScale, targetScale, t);

            yield return null;
        }
    }
}
