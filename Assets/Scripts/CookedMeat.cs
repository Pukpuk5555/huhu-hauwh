using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMeat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color meatColor;
    private Color burnMeatColor = new Color(0.4f, 0.2f, 0.1f);
    private float timer = 0f;
    private float meatBurnTime = 5f;

    private bool isBurning = false; 
    private bool isBurned = false; //is meat burned

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        meatColor = spriteRenderer.color;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fire"))
        {
            Debug.Log("Cooked Meat is on fire.");
            StartCoroutine(MeatBurn());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Fire"))
        {
            Debug.Log("Cooked Meat removed from fire.");
            StopCoroutine(MeatBurn());
        }
    }

    public IEnumerator MeatBurn()
    {
        Debug.Log("Meat start burning in 2 seconds...");
        yield return new WaitForSeconds(2f);

        isBurning = true;

        while (isBurning && !isBurned)
        {
            timer += Time.deltaTime;

            float burnProgress = Mathf.Clamp01(timer / meatBurnTime);
            spriteRenderer.color = Color.Lerp(meatColor, burnMeatColor, burnProgress);

            if(timer >= meatBurnTime)
            {
                isBurned = true;
                isBurning = false;
                Debug.Log("OverCooked Meat!");
            }

            yield return null;
        }
    }
}
