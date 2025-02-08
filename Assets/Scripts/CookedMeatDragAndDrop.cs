using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CookedMeatDragAndDrop : DragNDrop
{
    protected override void OnMouseDown()
    {
        base.OnMouseDown();
    }

    protected override void OnMouseDrag()
    {
        base.OnMouseDrag();
    }

    protected override void OnMouseUp()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Monkey"))
        {
            Customer customerScript = collision.GetComponent<Customer>();
            if(customerScript != null)
            {
                customerScript.ServeMeat(gameObject);
            }
        }
    }
}
