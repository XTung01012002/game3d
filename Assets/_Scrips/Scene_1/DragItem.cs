using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragItem : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    public GameLogic gameLogic;
    public GameObject myObjectPrefab;
    public Transform parentAfterDrag;
    private Vector3 initialPosition;

    public bool droppedOnGrid = false;
    private GameObject newObject;
    private Transform originalParent;

    void Start()
    {
        gameLogic = FindObjectOfType<GameLogic>();
        originalParent = transform.parent;
        initialPosition = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        gameLogic.OnBeginDragFirstWord();
        Debug.Log("OnBeginDrag");
        parentAfterDrag = transform.parent;
        transform.SetParent(GameObject.Find("Canvas").transform);
        transform.SetAsFirstSibling();

        // Tạo ra đối tượng mới nếu không phải là lần kéo thả lại
        if (!droppedOnGrid)
        {
            newObject = Instantiate(myObjectPrefab, initialPosition, Quaternion.identity, originalParent);
            newObject.GetComponent<DragItem>().droppedOnGrid = true;
        }

        droppedOnGrid = false;
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
        if (transform.parent == GameObject.Find("Canvas").transform)
        {
            // Nếu không thả vào ô hợp lệ, đưa item trở về vị trí ban đầu
            transform.SetParent(originalParent);
            transform.position = initialPosition;
            Destroy(newObject); // Xóa đối tượng mới được tạo ra khi không thả vào ô
        }
        else
        {
            // Đặt lại droppedOnGrid để đánh dấu rằng item này đã được thả vào lưới
            droppedOnGrid = true;
        }
    }
}
