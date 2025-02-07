using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public void ServeMeat(GameObject meat)
    {
        RawMeatDragAndDrop meatScript = meat.GetComponent<RawMeatDragAndDrop>();

        if(meatScript != null && meatScript.IsCooked())
        {
            Debug.Log("Monkey got meat! They satified.");
            Destroy(meat);
        }
        else
        {
            Debug.Log("Monkey does not get meat! They is angry");
        }
    }
}
