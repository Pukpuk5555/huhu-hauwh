using UnityEngine;
using System.Collections;

public class CookingMeat : IngredientsCooking
{
    [SerializeField] private float timeToCook = 7f;
    [SerializeField] private float timeToBurn = 8f;

    private Color overCookedColor = new Color(0.4f, 0.2f, 0.1f);
    private Color burnedColor = new Color(0.1f, 0.1f, 0.1f);
    private Coroutine cookingCoroutine;

    [SerializeField] private AudioSource grillAudio;

    protected override void Start()
    {
        base.Start();
        grillAudio = GetComponent<AudioSource>();
    }

    public override void StartCooking()
    {
        if(!isCooking)
        {
            base.StartCooking();
            cookingCoroutine = StartCoroutine(CookMeat());
        }
    }

    public override void StopCooking()
    {
        if(isCooking && cookingCoroutine != null)
        {
            StopCoroutine(cookingCoroutine);
            cookingCoroutine = null;
            Debug.Log("Cooking stopped manually.");
        }
        base.StopCooking();
        grillAudio.Stop();
    }

    private IEnumerator CookMeat()
    {
        float elapsedTime = 0f;
        Debug.Log("Meat is starting to cook...");
        grillAudio.Play();

        // Cooking phase
        while(elapsedTime < timeToCook)
        {
            Debug.Log("Meat is cooking.");
            spriteRenderer.color = Color.Lerp(rawColor, cookedColor,
                elapsedTime / timeToCook);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Cooked
        ingredientState = IngredientState.Cooked;
        spriteRenderer.sprite = cookedMeatSprite;
        spriteRenderer.color = Color.white;
        Debug.Log("Meat is cooked.");
        yield return new WaitForSeconds(2f);

        // Overcooking phase
        while(elapsedTime < timeToBurn)
        {
            ingredientState = IngredientState.OverCooked;
            Debug.Log("Meat is overcooked.");
            spriteRenderer.color = Color.Lerp(cookedColor, overCookedColor,
                (elapsedTime - timeToCook) / (timeToBurn - timeToCook));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Burned
        ingredientState = IngredientState.Burned;
        spriteRenderer.color = burnedColor;
        Debug.Log("Meat is burned.");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monkey"))
        {
            Debug.Log($"Hit {collision.gameObject.name}");

            Customer customerScript = collision.GetComponent<Customer>();
            if (customerScript != null)
            {
                customerScript.ServeMeat(gameObject);
            }
        }
    }
}
