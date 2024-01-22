using rpg.ui;
using UnityEngine;
using UnityEngine.UIElements;

public class GameMenuPresenter : MonoBehaviour
{
    private VisualElement m_Root;
    private GameMenuScreen m_GameMenuScreen;

    private void OnEnable()
    {
        m_Root = GetComponent<UIDocument>().rootVisualElement;
        m_GameMenuScreen = new GameMenuScreen(m_Root);
        
        Initialize();
    }

    private void Initialize()
    {
        NullRefChecker.Validate(this);

        if (!Coroutines.IsInitialized)
        {
            Coroutines.Initialize(this);
        }
        
        m_GameMenuScreen.Initialize();
        m_GameMenuScreen.RegisterCallbacks();
    }

    // TODO: Replace this with Event
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameMenu();
        }
    }

    private void ToggleGameMenu()
    {
        if (m_GameMenuScreen.IsHidden)
        {
            m_GameMenuScreen.Show();
        }
        else
        {
            m_GameMenuScreen.Hide();
        }
    }
}

public class GameMenuScreen : UIScreen
{
    private Button m_InventoryButton;
    private Button m_MapButton;
    private Button m_SkillsButton;

    private VisualElement m_InventoryContainer;
    private VisualElement m_MapContainer;
    private VisualElement m_SkillsContainer;

    public GameMenuScreen(VisualElement parentElement) : base(parentElement)
    {
        m_RootElement = parentElement;
        m_HideOnAwake = true;
        SetVisualElements();
    }

    private void SetVisualElements()
    {
        m_InventoryButton = m_RootElement.Q<Button>("inventory-tab-button");
        m_MapButton = m_RootElement.Q<Button>("map-tab-button");
        m_SkillsButton = m_RootElement.Q<Button>("skills-tab-button");
        
        m_InventoryContainer = m_RootElement.Q<VisualElement>("inventory-screen");
        m_MapContainer = m_RootElement.Q<VisualElement>("map-screen");
        m_SkillsContainer = m_RootElement.Q<VisualElement>("skills-screen");
    }

    public void RegisterCallbacks()
    {
        m_EventRegistry.RegisterCallback<ClickEvent>(m_InventoryButton, () =>
        {
            m_InventoryContainer.style.display = DisplayStyle.Flex;
            m_MapContainer.style.display = DisplayStyle.None;
            m_SkillsContainer.style.display = DisplayStyle.None;
        });
        
        m_EventRegistry.RegisterCallback<ClickEvent>(m_MapButton, () =>
        {
            m_InventoryContainer.style.display = DisplayStyle.None;
            m_MapContainer.style.display = DisplayStyle.Flex;
            m_SkillsContainer.style.display = DisplayStyle.None;
        });
        
        m_EventRegistry.RegisterCallback<ClickEvent>(m_SkillsButton, () =>
        {
            m_InventoryContainer.style.display = DisplayStyle.None;
            m_MapContainer.style.display = DisplayStyle.None;
            m_SkillsContainer.style.display = DisplayStyle.Flex;
        });
    }
}