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
        CookedMeatDragAndDrop meatScript = meat.GetComponent<CookedMeatDragAndDrop>();

        if(meatScript != null)
        {
            Debug.Log("Monkey got meat! They satified.");
            Destroy(meat);
            monkeyDeliciousAudio.Play();
        }
        else
        {
            Debug.Log("Monkey does not get meat! They is angry");
        }
    }
}
