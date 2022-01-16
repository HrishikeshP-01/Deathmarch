using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragWindow : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerDownHandler
{
    [SerializeField] private RectTransform dragRectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image backgroundImage;
    private Color backgroundColor;

    private void Awake()
    {
        // automatically get dragRectTransform
        if(dragRectTransform == null)
        {
            // For this to work the window must be the direct parent else modify code accordingly
            dragRectTransform = transform.parent.GetComponent<RectTransform>();
        }

        // automatically get canvas
        if (canvas == null)
        {
            Transform testCanvasTransform = transform.parent;
            // Loop till the root parent reached
            while (testCanvasTransform != null)
            {
                // if any of the parent objects in hierarchy has a canvas break loop
                canvas = testCanvasTransform.GetComponent<Canvas>();
                if (canvas != null)
                {
                    break;
                }
                testCanvasTransform = testCanvasTransform.parent;
            }
        }
        backgroundColor = backgroundImage.color;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        backgroundColor.a = .4f;
        backgroundImage.color = backgroundColor;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // only if you divide with canvas scale factor will the movement be scaled properly
        dragRectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        backgroundColor.a = 1f;
        backgroundImage.color = backgroundColor;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // To bring something in front just make it the last sibling of the parent object
        dragRectTransform.SetAsLastSibling();
    }
}
