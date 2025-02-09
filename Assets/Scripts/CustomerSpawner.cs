using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawner : MonoBehaviour
{
    public GameObject customerPrefab; // Prefab ลิง
    public Transform spawnPoint;      // จุดเกิดลิง
    public Transform counterPoint;    // จุดเคาน์เตอร์ (ลิงเดินมาหยุดที่นี่)
    public float spawnInterval = 5f;  // ระยะเวลาสุ่มลิง

    private void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while (true)
        {
            SpawnCustomer();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnCustomer()
    {
        GameObject customer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        CustomerBehavior behavior = customer.GetComponent<CustomerBehavior>();
        if (behavior != null)
        {
            behavior.SetTarget(counterPoint.position);
        }
    }
}
