using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private bool isDragging = false;
    private SpriteRenderer spriteRenderer;

    private IngredientsCooking cookingScript;

    public Transform fireObject;
    [SerializeField] private Transform meatPilePos;
    private static bool isFireOccupied = false;

    private int defaultLayer = 5;
    private int pickUpLayer = 6;

    private CursorManager cursorManager;

    [SerializeField] private AudioSource pickUpMeatAudio;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = defaultLayer;
        cookingScript = GetComponent<IngredientsCooking>();
        cursorManager = GetComponent<CursorManager>();
        pickUpMeatAudio = GetComponent<AudioSource>();
    }

    private void OnMouseDown()
    {
        cursorManager.SetHandCursor();

        if(isFireOccupied && cookingScript.IngredientState == IngredientState.Cooking)
        {
            Debug.Log("Cannot pick up meat while another one is cooking.");
            return;
        }

        isDragging = true;
        spriteRenderer.sortingOrder = pickUpLayer;

        if (pickUpMeatAudio != null)
            pickUpMeatAudio.Play();
    }

    private void OnMouseDrag()
    {
        cursorManager.SetHandCursor();

        if(isDragging)
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

        if(IsOverCookingArea() && !cookingScript.IsCooking)
        {
            transform.position = fireObject.transform.position;
            cookingScript.StartCooking();
            isFireOccupied = true;
        }
        else if(cookingScript.IngredientState == IngredientState.Cooked ||
                cookingScript.IngredientState == IngredientState.OverCooked ||
                cookingScript.IngredientState == IngredientState.Burned)
        {
            Debug.Log($"{gameObject.name} can be removed from bonfire.");
        }
        else
        {
            transform.position = meatPilePos.transform.position;
            Debug.Log($"{gameObject.name} returned to pile.");
        }
    }

    private bool IsOverCookingArea()
    {
        Collider2D hitCollider = Physics2D.OverlapPoint(transform.position);

        if(hitCollider != null && hitCollider.CompareTag("CookingArea"))
        {
            Debug.Log($"{gameObject.name} placed on cooking area.");
            return true;
        }
        return false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("CookingArea"))
        {
            Debug.Log($"{gameObject.name} removed from cooking area.");

            if(cookingScript.IsCooking)
            {
                cookingScript.StopCooking();
                isFireOccupied = false;
            }
        }
    }
}
