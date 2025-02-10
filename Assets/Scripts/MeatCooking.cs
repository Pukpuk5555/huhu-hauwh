using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatCooking : IngredientsCooking
{
    private float cookingTime = 7f;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        OnCooking();
    }

    public override void StartCooking()
    {
        base.StartCooking();
        Debug.Log($"Cooking time for this meat is {cookingTime} seconds.");
        Debug.Log("Meat is now cooking...");
    }

    public override void OnCooking()
    {
        if(isCooking)
        {
            cookingTimer += Time.deltaTime;
            float progress = Mathf.Clamp01(cookingTimer / cookingTime);
            spriteRenderer.color = Color.Lerp(rawColor, cookedColor, progress);

            if(progress >= 1f)
            {
                GameObject cookedMeat = Instantiate(cookedPrefab, transform.position, Quaternion.identity);
                cookedMeat.GetComponent<SpriteRenderer>().color = Color.white;
                Destroy(gameObject);
                isCooking = false;
                Debug.Log("Meat is ready to serve!");
            }
        }
    }
}
