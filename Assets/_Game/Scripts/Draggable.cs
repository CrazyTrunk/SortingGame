using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    public bool IsDragging;
    public Vector3 startPos;
    [SerializeField]
    private Collider2D _collider;
    private Item itemData;
    private Transform shelfTransform;
    private float _movementTime = 15f;
    private Nullable<Vector3> _movementDestination;
    private Slot currentSlotItem;
    private bool isTrigger = false;
    private bool canGoNext = false;
    private void Start()
    {
        itemData = GetComponent<Item>();
        currentSlotItem = itemData.slot;
        shelfTransform = GetComponentInParent<LayerController>().transform;
    }
    private void FixedUpdate()
    {
        if (IsDragging)
        {
            _movementDestination = null;
            transform.SetParent(shelfTransform);

        }

    }
    public void CheckingDrop()
    {

            transform.SetParent(currentSlotItem.transform);
            transform.position = startPos;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DropAvailable"))
        {
            _movementDestination = other.transform.position;
            Slot currentSlot = other.GetComponent<Slot>();
            if (currentSlot.isAvailable && !isTrigger)
            {
                SlotController slotController = other.GetComponentInParent<SlotController>();
                if (slotController != null)
                {
                    isTrigger = true;
                    slotController.AddItem(currentSlot, itemData);
                    currentSlot.isAvailable = false;
                    currentSlot.currentItemInslot = itemData;
                    currentSlot.tag = "DropUnavailable";
                    currentSlotItem = currentSlot;
                    itemData.transform.SetParent(currentSlotItem.transform);
                }
            }

        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropAvailable"))
        {
            isTrigger = false;
        }
    }
}
