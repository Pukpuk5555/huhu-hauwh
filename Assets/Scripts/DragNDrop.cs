        using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;

    private void OnMouseDown()
    {
        startPosition = transform.position;
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
    }

    private void OnMouseUp()
    {
        isDragging = false;
    }
}
