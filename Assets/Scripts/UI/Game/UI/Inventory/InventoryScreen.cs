using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class InventoryScreen : MonoBehaviour
{
    private InventorySO m_InventoryData;
    private VisualElement m_InventoryContainer;
    private List<InventorySlot> m_InventorySlots = new(); 
    
    private VisualElement m_GhostIcon;
    private bool m_IsDragging;
    private InventorySlot m_OriginalSlot;

    private void OnEnable()
    {
        SetVisualElements();
    }

    private void SetVisualElements()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        m_InventoryContainer = root.Q<VisualElement>("inventory-container");
        m_GhostIcon = root.Q<VisualElement>("ghost-icon");
        
        m_GhostIcon.RegisterCallback<PointerMoveEvent>(OnPointerMove);
        m_GhostIcon.RegisterCallback<PointerUpEvent>(OnPointerUp);
        
        InitializeInventory();
    }

    private void OnPointerUp(PointerUpEvent evt)
    {
        
    }

    private void OnPointerMove(PointerMoveEvent evt)
    {
        
    }

    private void InitializeInventory()
    {
        m_InventoryData = InventoryManager.Instance.InventoryData;
        
        // Add empty slots to the inventory
        for (var i = 0; i < m_InventoryData.MaxSlots; i++)
        {
            var slot = new InventorySlot();
            m_InventoryContainer.Add(slot);
            m_InventorySlots.Add(slot);
        }

        foreach (var itemData in m_InventoryData.Items)
        {
            var emptySlot = m_InventorySlots.FirstOrDefault(x => x.IsEmpty);
            emptySlot?.HoldItem(itemData);
        }
    }
    
    public void StartDrag(Vector2 position, InventorySlot originalSlot)
    {
        //Set tracking variables
        m_IsDragging = true;
        m_OriginalSlot = originalSlot;

        //Set the new position
        m_GhostIcon.style.top = position.y - m_GhostIcon.layout.height / 2;
        m_GhostIcon.style.left = position.x - m_GhostIcon.layout.width / 2;

        //Set the image
        m_GhostIcon.style.backgroundImage = GetItemByGuid(originalSlot.Id).Icon;

        //Flip the visibility on
        m_GhostIcon.style.visibility = Visibility.Visible;
    }
    
    public ItemData GetItemByGuid(string guid)
    {
        if (m_InventoryData.Items.Exists(x => x.Id.Equals(guid)))
        {
            return m_InventoryData.Items.Find(x => x.Id.Equals(guid));
        }
        
        return null;
    }
    
    // private void OnInventoryChanged(string[] itemGuid, InventoryChangeType change)
    // {
    //     foreach (string item in itemGuid)
    //     {
    //         if (change == InventoryChangeType.Pickup)
    //         {
    //             var emptySlot = m_InventorySlots.FirstOrDefault(x => x.Id.Equals(""));
    //             emptySlot?.HoldItem(GetItemByGuid(item));
    //         }
    //     }
    // }
}