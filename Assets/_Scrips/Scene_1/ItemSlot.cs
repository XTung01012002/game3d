using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public DragItem dragItems;

    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            dragItems = dropped.GetComponent<DragItem>();
            Debug.Log(dragItems.name);
            if (dragItems != null)
            {
                // Cập nhật parentAfterDrag của DragItem
                dragItems.parentAfterDrag = transform;
                dragItems.droppedOnGrid = true;
                // Di chuyển đối tượng vào ô hiện tại
                dropped.transform.SetParent(transform);
                dropped.transform.position = transform.position;
                Debug.Log(transform.name);
            }
        }
    }
}
