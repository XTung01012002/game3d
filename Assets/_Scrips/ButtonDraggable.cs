using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonDraggable : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    private bool isDragging = false;
    private RectTransform canvasRectTransform;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvasRectTransform = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Lưu vị trí ban đầu của button và bắt đầu kéo
        originalPosition = rectTransform.anchoredPosition;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Di chuyển button theo vị trí của chuột
        if (isDragging)
        {
            Vector2 localPointerPosition;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, eventData.position, eventData.pressEventCamera, out localPointerPosition))
            {
                // Kiểm tra xem vị trí mới có nằm trong canvas không
                float newX = Mathf.Clamp(localPointerPosition.x, -canvasRectTransform.rect.width / 2f + rectTransform.rect.width / 2f, canvasRectTransform.rect.width / 2f - rectTransform.rect.width / 2f);
                float newY = Mathf.Clamp(localPointerPosition.y, -canvasRectTransform.rect.height / 2f + rectTransform.rect.height / 2f, canvasRectTransform.rect.height / 2f - rectTransform.rect.height / 2f);
                rectTransform.localPosition = new Vector3(newX, newY, 0f);
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Kết thúc kéo và đặt lại vị trí của button khi thả chuột
        if (isDragging)
        {
            rectTransform.anchoredPosition = originalPosition;
            Debug.Log("quay ve");
            isDragging = false;
        }
    }
}
