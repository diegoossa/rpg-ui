using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    private VisualElement m_InventoryContainer;
    [SerializeField] private InventorySO InventoryData;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        m_InventoryContainer = root.Q<VisualElement>("inventory-container");
        
        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < InventoryData.MaxSlots; i++)
        {
            var slot = new InventorySlot
            {
                Id = i.ToString()
            };
            m_InventoryContainer.Add(slot);
        }
    }
}
