using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "InventoryData", menuName = "RPG/Inventory", order = 1)]
public class InventorySO : ScriptableObject
{
    public int MaxSlots = 30;
    [field: SerializeField] public List<ItemData> Items { set; get; } = new();
}

[Serializable]
public class ItemData
{
    [field: SerializeField] public string Id { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Texture2D Icon { get; set; }
    
}