using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour
{
    [SerializeField] private GameObject orderCanvas;
    [SerializeField] private Transform headPosition;
    [SerializeField] private GameObject orderUI;
    [SerializeField] private Slider satisfactionBar;

    private float satisfaction = 1f;
    private float decreaseRate = 0.2f;
    private bool isServe = false;

    [SerializeField] private AudioSource monkeyDeliciousAudio;
    [SerializeField] private AudioSource monkeyNeutralAudio;
    [SerializeField] private AudioSource monkeyAngryAudio;

    private void Start()
    {
        orderUI.SetActive(false);
        StartCoroutine(DecreaseSatisfaction());
        monkeyDeliciousAudio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (orderCanvas != null)
        {
            orderCanvas.transform.position = headPosition.position;
        }
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

    private IEnumerator DecreaseSatisfaction()
    {
        while (satisfaction > 0)
        {
            satisfaction -= decreaseRate * Time.deltaTime;
            satisfactionBar.value = satisfaction;
            yield return null;
        }

        if(!isServe)
        {
            Leave();
        }
    }

    public void ReceiveFood()
    {
        isServe = true;
        StopCoroutine(DecreaseSatisfaction());

        int score = Mathf.RoundToInt(satisfaction * 100);
        ScoreManager.Instance.AddScore(score);

        Leave();
    }

    public void Leave()
    {
        orderUI.SetActive(false);
        StartCoroutine(WalkAway());
    }

    private IEnumerator WalkAway()
    {
        float speed = 3f;
        Vector3 exitPoint = new Vector3(transform.position.x + 5,
            transform.position.y, transform.position.z);

        while(Vector3.Distance(transform.position, exitPoint) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, 
                exitPoint, speed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }
}
