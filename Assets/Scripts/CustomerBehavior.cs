using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CustomerBehavior : MonoBehaviour
{
    public float speed = 2f; // ความเร็วในการเดิน
    [SerializeField] Slider happySlider;
    [SerializeField] Image orderImage;
    private Vector3 targetPosition;
    private Vector3 newTargetPosition;
    private bool reachedCounter = false;
    bool setTargetPoint;
    bool ordered;
    public bool isOrder;
    public bool served;
    [SerializeField] int maxHappy = 15;
    float happy = 15;
    Customer customer;
    private void Start()
    {
        happy = maxHappy;
        happySlider.maxValue = maxHappy;
        happySlider.value = happy;
        happySlider.gameObject.SetActive(false);
        orderImage.gameObject.SetActive(false);
        customer = GetComponent<Customer>();
    }
    public void SetTarget(Vector3 target)
    {
        targetPosition = target;
    }

    private void Update()
    {

        happySlider.value = happy;
        if (ordered && !served)
        {
            happy-= Time.deltaTime;
            if (happy <= 0)
            {
                MoveToExitPoint(customer.exitPoint.position);
            }
        }

        if (!reachedCounter)
        {
            MoveToCounter();
        }
        if (setTargetPoint)
        {
            transform.position = Vector3.MoveTowards(transform.position, newTargetPosition, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, newTargetPosition) < 0.1f)
            {
                reachedCounter = true;
                setTargetPoint = false;
                if(!CustomerSpawner.instance.isTour)
                CustomerSpawner.instance.SpawnCustomer();
                CustomerSpawner.instance.RemoveCustomer(this);

                Destroy(gameObject);
            }
        }
    }

    public void MoveToCounter(Transform newPoint = null, bool newMove = false, bool isOrder = false)
    {
        if(newMove)
        reachedCounter = false;
        if (newPoint != null)
        {
            targetPosition = newPoint.position;
            this.isOrder = isOrder;
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            reachedCounter = true;
            OrderFood();
        }
    }
    public void MoveToExitPoint(Vector3 targetPoint)
    {
        served = true;
        newTargetPosition = targetPoint;
        setTargetPoint = true;
    }

    private void OrderFood()
    {
            Debug.Log($"Order has: " + isOrder);
        if (!isOrder)
        {
            return;
        }
            int ran = Random.Range(0, CustomerSpawner.instance.menus.Length);
        string foodOrder = CustomerSpawner.instance.menus[ran].menuName;
        if(CustomerSpawner.instance.menus[ran].sprite != null) 
        orderImage.sprite = CustomerSpawner.instance.menus[ran].sprite;
        Debug.Log($"Customer ordered: {foodOrder}");
        ordered = true;
        happySlider.gameObject.SetActive(true);
        orderImage.gameObject.SetActive(true);
    }
}
