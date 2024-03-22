using System.Collections.Generic;
using UnityEngine;

public class SlotController : MonoBehaviour
{
    [SerializeField] private List<Slot> slots = new List<Slot>();
    [SerializeField] private ItemDatas itemDatas;
    [SerializeField] private GameObject itemPrefab;
    private List<Item> listItem = new List<Item>();

    private void Start()
    {
        Init();
    }
    private void Init()
    {
        listItem.Clear();
        var randomIndex = Random.Range(0, 2);
        for (int i = 0; i < slots.Count; i++)
        {
            var item = Instantiate(itemPrefab);
            item.transform.SetParent(gameObject.transform);
            item.transform.localScale = Vector3.one;
            item.transform.position = slots[i].transform.position;
        }
    }
    public void AddItem(Slot slot, Item item)
    {
        if (IsSlotAvailable(slot))
        {
            listItem.Add(item);
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
