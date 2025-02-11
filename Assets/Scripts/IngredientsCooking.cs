using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum IngredientState
{
    Raw,
    Cooking,
    Cooked,
    OverCooked,
    Burned
}

public abstract class IngredientsCooking : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected Sprite cookedMeatSprite;
    protected Color rawColor;
    protected Color cookedColor = new Color(0.6f, 0.3f, 0.2f);
    protected float cookingTimer = 0f;
    protected bool isCooking = false;

    protected IngredientState ingredientState = IngredientState.Raw;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rawColor = spriteRenderer.color;
    }

    public virtual void StartCooking()
    {
        if (!isCooking)
        {
            isCooking = true;
            ingredientState = IngredientState.Cooking;
            cookingTimer = 0f;
            Debug.Log("Start Cooking.");
        }
    }

    public virtual void StopCooking()
    {
        if(isCooking)
        {
            isCooking = false;
            Debug.Log("Cooking stopped.");
        }
    }
}
