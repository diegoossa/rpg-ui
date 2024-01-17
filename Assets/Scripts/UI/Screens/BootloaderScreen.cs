using System;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(UIDocument))]
[ExecuteInEditMode]
public class BootloaderScreen : MonoBehaviour
{
    [SerializeField] private UIDocument m_Document;

    private VisualElement m_Root; 
    
    private void OnEnable()
    {
        // Reference Visual Elements (try-catch block suppresses errors on startup)
        try
        {
            SetVisualElements();
        }
        catch (NullReferenceException)
        {
            // Suppresses errors on startup
        }
    }
    
    private void SetVisualElements()
    {
        if (m_Document == null)
            m_Document = GetComponent<UIDocument>();

        m_Root = m_Document.rootVisualElement;
    }
}
