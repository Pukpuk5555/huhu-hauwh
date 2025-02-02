using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMeatDragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool isOverFire = false;
    private GameObject fireObject;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject meatPilePos;

    private int defaultLayer = 5;
    private int pickedUpLayer = 6;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = defaultLayer;
    }

    public void OnMouseDown()
    {
        isDragging = true;
    }

    private void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
        
    }

    private void OnMouseUp()
    {
        isDragging = false;
        spriteRenderer.sortingOrder = defaultLayer;

        if (isOverFire && fireObject != null)
        {
            Debug.Log("On Fire!!!!");
            transform.position = fireObject.transform.position;
            this.enabled = false;
            StartCooking();
        }
        else
        {
            transform.position = meatPilePos.transform.position;
            Debug.Log("Meat dropped.");
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            isOverFire = true;
            fireObject = collision.gameObject;
            Debug.Log("Meat is over fire!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            isOverFire = false;
            fireObject = null;
            Debug.Log("Meat left the fire!");
        }
    }

    private void StartCooking()
    {
        GetComponent<IngradientsCooking>().StartCooking();
    }
}
