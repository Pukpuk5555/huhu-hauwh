using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCooking : IngradientsCooking
{
    private float cookingTime = 7f;

    protected override void Start()
    {
        base.Start();
        Debug.Log($"Cooking time for this meat is {cookingTime} seconds.");
    }

    public override void StartCooking()
    {
        base.StartCooking();
        Debug.Log("Meat is now cooking...");
    }

    public override void OnCooking()
    {
        base.OnCooking();

        if(isCooking)
        {
            cookingTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(cookingTimer / cookingTime);
            spriteRenderer.color = Color.Lerp(rawColor, cookedColor, progress);

            if(progress >= 1f)
            {
                spriteRenderer.sprite = cookedSprite;
                isCooking = false;
                Debug.Log("Meat is ready to serve!");
            }
        }
    }
}
