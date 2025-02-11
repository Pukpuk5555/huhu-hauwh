using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookedMeat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color meatColor;
    private Color burnMeatColor = new Color(0.4f, 0.2f, 0.1f);
    private float timer = 0f;
    private float meatBurnTime = 5f;

    public IngredientState curMeatState = IngredientState.Cooked;

    private bool isBurning = false; 
    private bool isBurned = false; //is meat burned

    [SerializeField] private GameObject meatProgressBar;

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

        if (collision.CompareTag("Monkey"))
        {
            Customer customerScript = collision.GetComponent<Customer>();
            if (customerScript != null)
            {
                customerScript.ServeMeat(gameObject);
            }
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

            curMeatState = IngredientState.OverCooked;
            Debug.Log("OverCooked Meat!");

            if (timer >= meatBurnTime)
            {
                curMeatState = IngredientState.Burned;
                isBurned = true;
                isBurning = false;
                Debug.Log("MEAT HAS BURNED!");
            }

            yield return null;
        }
    }
}
