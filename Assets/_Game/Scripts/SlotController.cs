using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] private List<Slot> slots = new List<Slot>();
    private List<Item> listItem = new List<Item>();
    private void Start()
    {
        Init();
    }
    private void Init()
    {
        listItem.Clear();
    }
    public void AddItem(Slot slot, Item item)
    {
        if (IsSlotAvailable(slot))
        {
            listItem.Add(item);
        }
        if (CheckingSameItem())
        {
            Debug.Log("OK");
        }
        else
        {
            Debug.Log("Not meet");

        }
    }
    public bool IsSlotAvailable(Slot slot)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i] == slot)
            {
                return slots[i].isAvailable;
            }
        }
        return false;
    }
    public bool CheckingSameItem()
    {
        bool isSameItems = true;
        if (listItem.Count == 0 || listItem.Count < 3)
        {
            isSameItems = false;
            return isSameItems;
        }
        var firstItemId = listItem[0].id;
        for (int i = 0; i < listItem.Count; i++)
        {
            if (listItem[i].id != firstItemId)
            {
                isSameItems = false;
                break;
            }
        }
        return isSameItems;
    }

    public void RemoveItem(Item item)
    {
        listItem.Remove(item);
    }
}
