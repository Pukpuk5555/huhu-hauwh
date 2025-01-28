using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragNDrop : MonoBehaviour
{
    private Vector3 startPosition;
    private bool isDragging = false;
    private bool isOverFire = false;
    [SerializeField] private GameObject fireObject;

    private void OnMouseDown()
    {
        startPosition = transform.position;
        isDragging = true;
        Debug.Log("MouseDown");
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
        Debug.Log("MouseDrag");
    }

    private void OnMouseUp()
    {
        isDragging = false;
        Debug.Log("MouseUp");
        if (isOverFire)
        {
            Debug.Log("On Fire!!!!");
            transform.position = fireObject.transform.position;
            StartCooking();
        }
        else
        {
            transform.position = startPosition;
            Debug.Log("Ingredient returned to start position.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fire"))
        {
            isOverFire = true;
            isDragging = false;
            fireObject = collision.gameObject;
            Debug.Log("Meat is over fire!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            isOverFire = false;
            isDragging = false;
            fireObject = null;
            Debug.Log("Meat left the fire!");
        }
    }

    private void StartCooking()
    {
        GetComponent<IngradientsCooking>().StartCooking();
    }
}
