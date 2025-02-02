using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DragNDrop : MonoBehaviour
{
    protected Vector3 startPosition;
    protected bool isDragging = false;

    protected virtual void OnMouseDown()
    {
        startPosition = transform.position;
        isDragging = true;
        Debug.Log($"Mouse Down on {gameObject.name}.");
    }

    protected virtual void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
        Debug.Log($"Mouse is dragging {gameObject.name}.");
    }

    protected virtual void OnMouseUp()
    {
        isDragging = false;
        Debug.Log($"Nothing on drag.");
    }
}
