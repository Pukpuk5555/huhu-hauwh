using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RawMeatDragAndDrop : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;
    private IngredientsCooking cookingScript;
    private SpriteRenderer spriteRenderer;

    [SerializeField] private Transform originalPos;
    private static bool isFireOccupied = false;

    private int defaultLayer = 5;
    private int pickedUpLayer = 6;

    private CursorManager cursorManager;

    [SerializeField] private AudioSource pickupMeatAudio;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = defaultLayer;
        cookingScript = GetComponent<IngredientsCooking>();
        pickupMeatAudio = GetComponent<AudioSource>();
        cursorManager = FindObjectOfType<CursorManager>();
    }

    public void OnMouseDown()
    {
        cursorManager.SetHandCursor();

        if(isFireOccupied && !cookingScript.IsCooked())
        {
            Debug.Log("Cannot pick up meat while another one is cooking.");
            return;
        }

        isDragging = true;
        spriteRenderer.sortingOrder = pickedUpLayer;

        if (pickupMeatAudio != null)
            pickupMeatAudio.Play();
    }

    private void OnMouseDrag()
    {
        cursorManager.SetHandCursor();

        if (isDragging)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
    }

    private void OnMouseUp()
    {
        cursorManager.SetHandCursor();

        isDragging = false;
        spriteRenderer.sortingOrder = defaultLayer;

        if(fireObject != null && !cookingScript.IsCooking())
        {
            transform.position = fireObject.transform.position;
            cookingScript.StartCooking();
            isFireOccupied = true;
        }
        else if(cookingScript.IsCooked())
        {
            Debug.Log($"{gameObject.name} can remove from bonfire.");
        }
        else
        {
            transform.position = meatPilePos.transform.position;
            Debug.Log($"{gameObject.name} return to pile.");
        }
    }

    private Vector3 GetCookingAreaPosition()
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);
        return hitCollider != null ? hitCollider.transform.position : transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            fireObject = collision.gameObject;
            Debug.Log($"{gameObject.name} is over fire!");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Fire"))
        {
            Debug.Log($"{gameObject.name} left the fire!");

            if(ingredientState = IngredientState.Cooking)
            {
                cookingScript.StopCooking();
                isFireOccupied = false;
            }
        }
    }
}
