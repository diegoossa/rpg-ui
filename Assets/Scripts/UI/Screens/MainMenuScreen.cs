using UnityEngine.UIElements;

namespace rpg.ui
{
    public class MainMenuScreen : UIScreen
    {
        private Button m_NewGameButton;
        
        public MainMenuScreen(VisualElement parentElement) : base(parentElement)
        {
            SetVisualElements();
            RegisterCallbacks();
        }

        private void SetVisualElements()
        {
            m_NewGameButton = m_RootElement.Q<Button>("menu__button-new-game");
        }

        private void RegisterCallbacks()
        {
            m_EventRegistry.RegisterCallback<ClickEvent>(m_NewGameButton, evt => ShowCharacterSelection());
        }

        private void ShowCharacterSelection()
        {
            UIEvents.CharacterSelectionShown?.Invoke();
            SceneEvents.LoadSceneByPath("CharacterSelection");
        }
    }
}