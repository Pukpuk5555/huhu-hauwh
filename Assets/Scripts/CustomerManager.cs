using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] waitingSpots;

    private Queue<Customer> waitingQueue = new Queue<Customer>();
    private int maxCustomer = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SpawnCustomers());
    }

    private IEnumerator SpawnCustomers()
    {
        while(true)
        {
            if(waitingQueue.Count < maxCustomer)
            {
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject newCustomerObj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
                Customer newCustomer = newCustomerObj.GetComponent<Customer>();
                waitingQueue.Enqueue(newCustomer);


            }
            yield return new WaitForSeconds(Random.Range(3f, 6f));
        }
    }

    private IEnumerator MoveCustomerToPosition(Customer customer, Vector3 targetPosition)
    {
        float speed = 2f;
        while(Vector3.Distance(customer.transform.position, targetPosition) > 0.1f)
        {
            customer.transform.position = Vector3.MoveTowards(customer.transform.position,
                targetPosition, speed * Time.deltaTime);
            yield return null;
        }
    }

    public void ServeCustomer(Customer serveCustomer)
    {
        if(waitingQueue.Count > 0 && waitingQueue.Peek() == serveCustomer)
        {
            waitingQueue.Dequeue();
            RearrangeQueue();
        }
    }

    private void RearrangeQueue()
    {
        int index = 0;
        foreach(Customer customer in waitingQueue)
        {
            StartCoroutine(MoveCustomerToPosition(customer, waitingSpots[index].position));
            index++;
        }
    }
}
