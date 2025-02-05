using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawMeatDragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private bool isCooked = false;
    private bool isMeatOnFire = false;
    private GameObject fireObject;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject meatPilePos;
    private IngradientsCooking cookingScript;

    private int defaultLayer = 5;
    private int pickedUpLayer = 6;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = defaultLayer;
        cookingScript = GetComponent<IngradientsCooking>();
    }

    public void OnMouseDown()
    {
        if(isMeatOnFire)
        {
            Debug.Log("Cannot pick up meat while another one is cooking.");
            return;
        }

        isDragging = true;
        spriteRenderer.sortingOrder = pickedUpLayer;
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

        if(!isCooked && isMeatOnFire)
        {
            Debug.Log("Meat is cooking, cannot move.");
            return;
        }

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
        }
        else
        {
            transform.position = meatPilePos.transform.position;
            Debug.Log("Cannot place meat while another one is cooking.");
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

            if(isMeatOnFire && !isCooked)
            {
                isMeatOnFire = false;
                cookingScript.StopCooking();
                Debug.Log("Meat remove forem fire before it was fully cooked.");
            }
        }
    }

    private void StartCooking()
    {
        cookingScript.StartCooking();
        Invoke(nameof(FinishCooking), 7f);
    }

    private void FinishCooking()
    {
        isCooked = true;
        isMeatOnFire = false;
        Debug.Log("Meat is cooked and can be picked up.");
    }
}
