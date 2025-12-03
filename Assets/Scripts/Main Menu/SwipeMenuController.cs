using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwipeMenuController : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    [Header("References")]
    public ScrollRect scrollRect;

    [Header("Settings")]
    public int totalPages = 4;
    public float snapSpeed = 10f;

    private int currentPage = 0;
    private float dragStartPos = 0f;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragStartPos = scrollRect.horizontalNormalizedPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float dragDelta = scrollRect.horizontalNormalizedPosition - dragStartPos;

        if (dragDelta > 0.05f && currentPage > 0)
        {
            currentPage--;
        }
        else if (dragDelta < -0.05f && currentPage < totalPages - 1)
        {
            currentPage++;
        }
    }

    void Update()
    {
        float target = (float)currentPage / (totalPages - 1);
        scrollRect.horizontalNormalizedPosition =
            Mathf.Lerp(scrollRect.horizontalNormalizedPosition, target, Time.deltaTime * snapSpeed);
    }
}
