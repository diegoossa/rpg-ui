using UnityEngine;
using UnityEngine.UIElements;

public class MapScreen : MonoBehaviour
{
    private VisualElement m_MapImage;

    private void OnEnable()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        m_MapImage = root.Q<VisualElement>("map-image");
        m_MapImage.AddManipulator(new DragManipulator());
    }
}