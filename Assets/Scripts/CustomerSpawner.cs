using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public static CustomerSpawner instance;

    public GameObject customerPrefab; // Prefab ลิง
    public Transform spawnPoint;      // จุดเกิดลิง
    public Transform counterPoint;    // จุดเคาน์เตอร์ (ลิงเดินมาหยุดที่นี่)
    public float spawnInterval = 5f;  // ระยะเวลาสุ่มลิง
    public bool isTour;
    [SerializeField] Transform[] counterPoints;
    public List<CustomerBehavior> customers;
    [SerializeField] int maxCustomers = 4;
    public Menus[] menus;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //StartCoroutine(SpawnCustomers());
        if (!isTour)
            SpawnCustomer();
        else
        {
            StartCoroutine(SpawnCustomers());
        }
    }

    private IEnumerator SpawnCustomers()
    {
        for (int i = 0; i < maxCustomers; i++)
        {
            if (customers.Count >= 4)
            {
                i--;
            }
            else
            {
                SpawnCustomer();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    public void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerBehavior behavior = customer.GetComponent<CustomerBehavior>();
        customers.Add(behavior);
        if (behavior != null)
        {
            behavior.SetTarget(counterPoint.position);
        }
        Refresh();
    }
    public void RemoveCustomer(CustomerBehavior customer)
    {
        customers.Remove(customer);
        Refresh();
    }
    void Refresh()
    {
        for (int i = 0; i < customers.Count; i++)
        {
            if(i == 0)
            customers[i].MoveToCounter(counterPoints[i],true,true);
            else
            customers[i].MoveToCounter(counterPoints[i],true);
        }
    }
}
[System.Serializable]
public class Menus
{
    public string menuName;
    public Sprite sprite;
}
