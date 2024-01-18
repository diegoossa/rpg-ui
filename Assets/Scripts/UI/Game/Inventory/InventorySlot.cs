using UnityEngine;
using UnityEngine.UIElements;

[UxmlElement]
public partial class InventorySlot : VisualElement
{
    [UxmlAttribute]
    public Image Icon { get; set; }
    [UxmlAttribute]
    public string Id { get; set; }
    
    public InventorySlot()
    {
        Icon = new Image();
        Add(Icon);

        //Add USS style properties to the elements
        Icon.AddToClassList("slot-icon");
        AddToClassList("slot-container");
    }
}