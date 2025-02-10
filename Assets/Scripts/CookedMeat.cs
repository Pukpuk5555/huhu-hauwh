using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMeat : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Color meatColor;
    private Color burnMeatColor = new Color(0.6f, 0.3f, 0.2f);
    private float timer = 0f;
    private float meatBurnTime = 12f;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        meatColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Fire"))
        {
            Invoke(nameof(MeatBurn), 2f);
        }
    }

    private void MeatBurn()
    {
        timer += Time.deltaTime;
        float meatBurnProgress = Mathf.Clamp01(timer / meatBurnTime);
        spriteRenderer.color = Color.Lerp(meatColor, burnMeatColor, meatBurnProgress);
    }
}
