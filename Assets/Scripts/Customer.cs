using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public void ServeMeat(GameObject meat)
    {
        CookedMeatDragAndDrop meatScript = meat.GetComponent<CookedMeatDragAndDrop>();

        if(meatScript != null)
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
