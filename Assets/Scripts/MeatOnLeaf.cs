using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatOnLeaf : MonoBehaviour
{
    [SerializeField] private GameObject rawMeatPrefab;
    [SerializeField] private Transform rawMeatPos;
    [SerializeField] private LayerMask meatLayer;

    private void OnMouseDown()
    {
        Debug.Log($"Mouse Down on {gameObject.name}.");
        SpawnMeat();
    }

    private void SpawnMeat()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f, meatLayer);

        if(hit.collider != null && hit.collider.gameObject == gameObject)
        {
            GameObject rawMeat = Instantiate(rawMeatPrefab, rawMeatPos.position, Quaternion.identity);
            rawMeat.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}
