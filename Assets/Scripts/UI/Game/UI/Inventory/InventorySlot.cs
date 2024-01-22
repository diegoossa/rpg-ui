using UnityEngine.UIElements;

[UxmlElement]
public partial class InventorySlot : VisualElement
{
   public string Id { get; set; }
   public Image Icon { get; set; }
   public bool IsEmpty => string.IsNullOrEmpty(Id);
    
    public InventorySlot()
    {
        // Create Image element
        Icon = new Image();
        Add(Icon);
        
        Icon.AddManipulator(new DragManipulator());
        
        //Add USS style properties to the elements
        Icon.AddToClassList("slot-icon");
        AddToClassList("slot-container");
    }
    
    public void HoldItem(ItemData item)
    {
        Icon.image = item.Icon;
        Id = item.Id;
    }
    
    public void DropItem()
    {
        Id = "";
        Icon.image = null;
    }
}