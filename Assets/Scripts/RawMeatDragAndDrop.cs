using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMeatDragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool isCooked = false;
    private static bool isMeatOnFire = false;
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
        if(isCooked)
        {
            isDragging = true;
        }
        else if(!isMeatOnFire)
        {
            isDragging = true;
            Debug.Log("Meat picked up.");
        }
        else
        {
            Debug.Log("Cannot pick up meat while another one is cooking.");
        }
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

        if(!isCooked && fireObject != null && !isMeatOnFire)
        {
            isMeatOnFire = true;
            transform.position = fireObject.transform.position;
            Debug.Log("Meat is place on fire.");
            StartCooking();
        }
        else if(isCooked)
        {
            isMeatOnFire = false;
            transform.position = meatPilePos.transform.position;
            Debug.Log("Cannot place meat while another one is cooking.");
        }
        else
        {
            transform.position = meatPilePos.transform.position;
            Debug.Log("Meat back to meat pile.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            fireObject = collision.gameObject;
            Debug.Log("Meat is over fire!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            fireObject = null;
            Debug.Log("Meat left the fire!");
        }
    }

    private void StartCooking()
    {
        GetComponent<IngradientsCooking>().StartCooking();
        Invoke(nameof(FinishCooking), 7f);
    }

    private void FinishCooking()
    {
        isCooked = true;
        isMeatOnFire = false;
        Debug.Log("Meat is cooked and can be picked up.");
    }
}
