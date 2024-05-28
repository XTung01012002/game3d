using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class btnSlot : MonoBehaviour, IDropHandler 
{
  

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition =
                transform.GetComponent<RectTransform>().anchoredPosition;
            eventData.pointerDrag.GetComponent<Image>().color = Color.green;
        }
        
    }
}
