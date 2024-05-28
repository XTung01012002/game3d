using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 originalPosition;
    public TextMeshProUGUI draggedText;
    private logic logicScript;

    private float startTime = 0f;

    private static bool isStartTimeSet = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        draggedText = GetComponentInChildren<TextMeshProUGUI>();
        logicScript = FindObjectOfType<logic>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isStartTimeSet) // Chỉ gán startTime lần đầu tiên bắt đầu kéo
        {
            startTime = Time.time;
            isStartTimeSet = true;  // Đánh dấu rằng thời gian đã được gán
            Debug.Log("Start time: " + startTime);
            logicScript.SetStartTime(startTime);
        }

    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Vector3.Distance(transform.position, Camera.main.transform.position);
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(worldPos.x, worldPos.y, transform.position.z);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = originalPosition;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logicScript.HandleCollision(collision, draggedText);
    }
}
