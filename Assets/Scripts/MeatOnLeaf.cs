using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatOnLeaf : DragNDrop
{
    [SerializeField] private GameObject rawMeatPrefab;
    [SerializeField] private Transform rawMeatPos;

    protected override void OnMouseDown()
    {
        base.OnMouseDown();
        GameObject rawMeat = Instantiate(rawMeatPrefab, rawMeatPos.position, Quaternion.identity);
    }
}
