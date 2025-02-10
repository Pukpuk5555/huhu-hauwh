using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    private AudioSource monkeyDeliciousAudio;

    private void Start()
    {
        monkeyDeliciousAudio = GetComponent<AudioSource>();
    }

    public void ServeMeat(GameObject meat)
    {
        CookedMeat meatScript = meat.GetComponent<CookedMeat>();

        if(meatScript != null)
        {
            switch (meatScript.curMeatState)
            {
                case IngredientState.Cooked:
                    Debug.Log("Monkey got cooked meat. Monkey's satisfied.");
                    Destroy(meat);
                    monkeyDeliciousAudio.Play();
                    break;
                case IngredientState.OverCooked:
                    Debug.Log("Monkey got overcooked meat. Monkey's neutral.");
                    Destroy(meat);
                    break;
                case IngredientState.Burned:
                    Debug.Log("Monkey got BURN MEAT! Monkey's ANGRY!!!");
                    Destroy(meat);
                    break;

                default:
                    Debug.Log("state not match.");
                    break;
            }
        }
    }
}
