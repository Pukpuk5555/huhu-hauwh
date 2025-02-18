using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField] private AudioSource monkeyDeliciousAudio;
    [SerializeField] private AudioSource monkeyNeutralAudio;
    [SerializeField] private AudioSource monkeyAngryAudio;
    public Transform exitPoint;
    CustomerBehavior monkey;

    private void Start()
    {
        exitPoint = GameObject.Find("ExitPoint").transform;
        monkeyDeliciousAudio = GetComponent<AudioSource>();
        monkey = GetComponent<CustomerBehavior>();
    }

    public void ServeMeat(GameObject meat)
    {
        if (!monkey.isOrder) return;
        CookingMeat meatScript = meat.GetComponent<CookingMeat>();
        if (meatScript != null)
        {
            Vector3 targetPosition = exitPoint.position;
            switch (meatScript.IngredientState)
            {
                case IngredientState.Cooked:
                    Debug.Log("Monkey got cooked meat. Monkey's satisfied.");
                    Destroy(meat);
                    monkeyDeliciousAudio.Play();
            monkey.MoveToExitPoint(targetPosition);
                    break;
                case IngredientState.OverCooked:
                    Debug.Log("Monkey got overcooked meat. Monkey's neutral.");
                    Destroy(meat);
                    monkeyNeutralAudio.Play();
            monkey.MoveToExitPoint(targetPosition);
                    break;
                case IngredientState.Burned:
                    Debug.Log("Monkey got BURN MEAT! Monkey's ANGRY!!!");
                    Destroy(meat);
                    monkeyAngryAudio.Play();
            monkey.MoveToExitPoint(targetPosition);
                    break;

                default:
                    Debug.Log("state not match.");
                    break;
            }
        }
    }
}
