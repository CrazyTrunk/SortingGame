using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ItemData", menuName = "Items/ItemData")]
[Serializable]
public class ItemData : ScriptableObject
{
    public int id;
    public string itemName;
    public Sprite image;
}