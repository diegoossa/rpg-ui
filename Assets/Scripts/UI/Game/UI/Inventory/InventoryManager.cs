public class InventoryManager : Singleton<InventoryManager>
{
    public InventorySO InventoryData;
    
    // private List<InventorySlot> m_InventorySlots = new();
    // private static Dictionary<string, ItemData> m_ItemDatabase = new();
    // public static ItemData GetItemByGuid(string guid)
    // {
    //     if (m_ItemDatabase.TryGetValue(guid, out var item))
    //     {
    //         return item;
    //     }
    //
    //     return null;
    // }
    //
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