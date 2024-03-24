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
        for (int i = 0; i <2; i++)
        {
            var item = Instantiate(itemPrefab).GetComponent<Item>();
            item.transform.SetParent(slots[i].transform);
            item.transform.localScale = Vector3.one;
            item.transform.localPosition = Vector3.zero;
            item.slot = slots[i];
        }
    }
    public void AddItem(Slot slot, Item item)
    {
        listItem.Add(item);

    }


    public void RemoveItem(Item item)
    {
        listItem.Remove(item);
    }
}
