using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngradientsCooking : MonoBehaviour
{
    protected SpriteRenderer spriteRenderer;

    [SerializeField] protected GameObject cookedPrefab;
    protected Color rawColor;
    protected Color cookedColor = new Color(0.6f, 0.3f, 0.2f);
    protected float cookingTimer = 0f;
    protected bool isCooking = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rawColor = spriteRenderer.color;
    }

    protected virtual void Update()
    {
        OnCooking();
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

    public virtual void StopCooking()
    {
        if(isCooking)
        {
            isCooking = false;
            Debug.Log("Cooking processs stopped.");
        }
    }
}
