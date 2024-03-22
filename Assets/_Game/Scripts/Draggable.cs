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
    [SerializeField]
    private DragController _dragController;
    private Item itemData;

    private float _movementTime = 15f;
    private Nullable<Vector3> _movementDestination;
    private void Start()
    {
        itemData = GetComponent<Item>();
    }
    private void FixedUpdate()
    {
        if (_movementDestination.HasValue)
        {
            if (IsDragging)
            {
                _movementDestination = null;
                return;
            }
            if (transform.position == _movementDestination)
            {
                gameObject.layer = Layer.Default;
                _movementDestination = null;

            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _movementDestination.Value, _movementTime * Time.deltaTime);
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Draggable colliderDraggable = other.GetComponent<Draggable>();
        if (colliderDraggable != null && _dragController.LastDragged.gameObject == gameObject)
        {
            ColliderDistance2D colliderDistance2D = other.Distance(_collider);
            Vector3 diff = new Vector3(colliderDistance2D.normal.x, colliderDistance2D.normal.y) * colliderDistance2D.distance;
            transform.position -= diff;
        }
        if (other.CompareTag("DropAvailable"))
        {
            _movementDestination = other.transform.position;
            Slot currentSlot = other.GetComponent<Slot>();
            if (currentSlot.isAvailable)
            {
                SlotController slotController = other.GetComponentInParent<SlotController>();
                if (slotController != null)
                {
                    slotController.AddItem(currentSlot, itemData);
                    currentSlot.isAvailable = false;
                    currentSlot.currentItemInslot = itemData;
                    currentSlot.tag = "DropUnavailable";
                }
            }

        }
        else if (other.CompareTag("DropUnavailable"))
        {
            _movementDestination = startPos;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DropUnavailable"))
        {
            Slot currentSlot = other.GetComponent<Slot>();
            if (!currentSlot.isAvailable && currentSlot.currentItemInslot == itemData)
            {
                SlotController slotController = other.GetComponentInParent<SlotController>();
                if (slotController != null)
                {
                    slotController.RemoveItem(itemData);
                    currentSlot.isAvailable = true;
                    currentSlot.tag = "DropAvailable";
                }
            }
        }
    }
}
