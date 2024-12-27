using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.Model;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;

    private void OnTriggerEnter2D(Collider2D collision)  // Code debugging again
    {
        if (inventoryData == null)
        {
            Debug.LogError("PickUpSystem: inventoryData is not assigned.");
            return;
        }

        Item item = collision.GetComponent<Item>();
        if (item == null)
        {
            Debug.LogWarning("PickUpSystem: No Item component found on collided object.");
            return;
        }

        if (item.InventoryItem == null)
        {
            Debug.LogError("PickUpSystem: InventoryItem is null on the picked-up item.");
            return;
        }

        int reminder = inventoryData.AddItem(item.InventoryItem, item.Quantity);
        if (reminder == 0)
            item.DestroyItem();
        else    
            item.Quantity = reminder;
    }

}
