using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatOnLeaf : MonoBehaviour
{
    [SerializeField] private GameObject rawMeatPrefab; //new meat that will grill
    [SerializeField] private Transform rawMeatPos; //raw meat pile
    [SerializeField] private LayerMask meatLayer;

    [SerializeField] private GameObject draggingMeat; //new meat ready to drag to fire

    private void Update()
    {
        if(draggingMeat != null)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            draggingMeat.transform.position = mousePosition;
        }
    }

    private void OnMouseDown()
    {
        Debug.Log($"Mouse Down on {gameObject.name}.");
        SpawnMeat();
    }

    private void OnMouseUp()
    {
        draggingMeat = null;
    }

    private void SpawnMeat()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, meatLayer);

        if(hit.collider != null && hit.collider.gameObject == gameObject)
        {
            //draggingMeat = Instantiate(rawMeatPrefab, rawMeatPos.position, Quaternion.identity);
            draggingMeat.GetComponent<SpriteRenderer>().sortingOrder = 6;
            draggingMeat.GetComponent<RawMeatDragAndDrop>().OnMouseDown();
        }
    }
}
