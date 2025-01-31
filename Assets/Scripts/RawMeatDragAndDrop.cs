using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMeatDragAndDrop : DragNDrop
{
    private bool isOverFire = false;
    private GameObject fireObject;

    private SpriteRenderer spriteRenderer;

    private int defaultLayer = 4;
    private int pickedUpLayer = 5;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = defaultLayer;
    }

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        spriteRenderer.sortingOrder = pickedUpLayer;
    }

    protected override void OnMouseDrag()
    {
        base.OnMouseDrag();
    }

    protected override void OnMouseUp()
    {
        base.OnMouseUp();
        spriteRenderer.sortingOrder = defaultLayer;

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
        if (collision.CompareTag("Fire"))
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
