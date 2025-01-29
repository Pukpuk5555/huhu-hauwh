using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMeatDragAndDrop : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;

    private void OnMouseDown()
    {
        startPosition = transform.position;
        isDragging = true;
        Debug.Log($"Mouse Down on {gameObject.name}.");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
        Debug.Log($"Mouse is dragging {gameObject.name}.");
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Debug.Log($"Nothing on drag.");
    }
}
