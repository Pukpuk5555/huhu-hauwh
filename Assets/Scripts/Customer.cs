using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private AudioSource monkeyDeliciousAudio;
    [SerializeField] private AudioSource monkeyNeutralAudio;
    [SerializeField] private AudioSource monkeyAngryAudio;

    private void Start()
    {
        monkeyDeliciousAudio = GetComponent<AudioSource>();
    }

    public void ServeMeat(GameObject meat)
    {
        CookingMeat meatScript = meat.GetComponent<CookingMeat>();

        if(meatScript != null)
        {
            switch (meatScript.IngredientState)
            {
                case IngredientState.Cooked:
                    Debug.Log("Monkey got cooked meat. Monkey's satisfied.");
                    Destroy(meat);
                    monkeyDeliciousAudio.Play();
                    break;
                case IngredientState.OverCooked:
                    Debug.Log("Monkey got overcooked meat. Monkey's neutral.");
                    Destroy(meat);
                    monkeyNeutralAudio.Play();
                    break;
                case IngredientState.Burned:
                    Debug.Log("Monkey got BURN MEAT! Monkey's ANGRY!!!");
                    Destroy(meat);
                    monkeyAngryAudio.Play();
                    break;

                default:
                    Debug.Log("state not match.");
                    break;
            }
        }
    }
}
