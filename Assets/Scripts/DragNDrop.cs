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
        if(CheckOnFire())
        {
            StartCooking();
        }
        else
        {
            transform.position = startPosition;
        }
    }

    private bool CheckOnFire()
    {
        Collider2D fireCollider = Physics2D.OverlapPoint(transform.position);
        return fireCollider != null && fireCollider.CompareTag("Fire");
    }

    private void StartCooking()
    {
        GetComponent<IngradientsCooking>().StartCooking();
    }
}
