using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehavior : MonoBehaviour
{
    public float speed = 2f; // ความเร็วในการเดิน
    private Vector3 targetPosition;
    private bool reachedCounter = false;
    private string[] menuItems = { "CookedMeat"}; // รายการเมนู

    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {
        if (!reachedCounter)
        {
            MoveToCounter();
        }
    }

    private void MoveToCounter()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            reachedCounter = true;
            OrderFood();
        }
    }

    private void OrderFood()
    {
        string foodOrder = menuItems[Random.Range(0, menuItems.Length)];
        Debug.Log($"Customer ordered: {foodOrder}");
    }
}
