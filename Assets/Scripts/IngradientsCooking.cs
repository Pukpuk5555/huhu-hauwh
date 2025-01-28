using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngradientsCooking : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    protected Sprite cookedSprite;
    protected Color rawColor;
    protected Color cookedColor = new Color(0.6f, 0.3f, 0.2f);
    protected float cookingTimer = 0f;
    protected bool isCooking = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rawColor = spriteRenderer.color;
    }

    public virtual void StartCooking()
    {
        if(!isCooking)
        {
            isCooking = true;
            cookingTimer = 0f;
            Debug.Log("Start Cooking");
        }
    }

    public virtual void OnCooking()
    {
        
    }
}
