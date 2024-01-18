using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InventoryData", menuName = "RPG/Inventory", order = 1)]
public class InventorySO : ScriptableObject
{
    public int MaxSlots = 30;
    public List<ItemData> Items = new();
}

[Serializable]
public class ItemData
{
    [field: SerializeField] public int Id { get; set; }
    [field: SerializeField] public string Name { get; set; }
    [field: SerializeField] public Sprite Icon { get; set; }
}