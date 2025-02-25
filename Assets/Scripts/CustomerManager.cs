using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CustomerManager : MonoBehaviour
{
    public static CustomerManager instance;

    [SerializeField] private GameObject customerPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Transform[] waitingSpots;

    private Queue<Customer> waitingQueue = new Queue<Customer>();
    private int maxCustomer = 3;

    private void Awake()
    {
        instance = this;
    }

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
                Debug.Log(maxCustomer);
                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                GameObject newCustomerObj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
                Customer newCustomer = newCustomerObj.GetComponent<Customer>();
                waitingQueue.Enqueue(newCustomer);

                StartCoroutine(MoveCustomerToPosition(newCustomer, waitingSpots[waitingQueue.Count - 1].position));
                Debug.Log("New money is walking to order.");
            }
            yield return new WaitForSeconds(Random.Range(3f, 6f));
            Debug.Log("Waiting for new monkey.");
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

    public void ServeCustomer(Customer servedCustomer)
    {
        if(waitingQueue.Count > 0 && waitingQueue.Peek() == servedCustomer)
        {
            waitingQueue.Dequeue();
            servedCustomer.Leave();
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
