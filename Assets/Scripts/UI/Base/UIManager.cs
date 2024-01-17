using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace rpg.ui
{
    /// <summary>
    /// The UI Manager manages the UI screens (View base class) using GameEvents paired
    /// to each View screen. A stack maintains a history of previously shown screens, so
    /// the UI Manager can "go back" until it reaches the default UI screen, the home screen.
    ///
    /// To add a new UIScreen under the UIManager's management:
    ///    -Define a new UIScreen field
    ///    -Create a new instance of that screen in Initialize (e.g. new SplashScreen(root.Q<VisualElement>("splash__container"));
    ///    -Register the UIScreen in the RegisterScreens method
    ///    -Subscribe/unsubscribe from the appropriate UIEvent to show the screen
    ///
    /// Alternatively, use Reflection to add the UIScreen to the RegisterScreens method
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [Tooltip("Required UI Document")] [SerializeField]
        private UIDocument m_Document;

        private UIScreen m_SplashScreen;
        private UIScreen m_StartScreen;
        private UIScreen m_HomeScreen;
        private UIScreen m_SettingsScreen;
        private UIScreen m_CharacterSelectionScreen;
        private UIScreen m_GameScreen;
        private UIScreen m_PauseScreen;

        private UIScreen m_CurrentScreen;
        private Stack<UIScreen> m_History = new();
        private List<UIScreen> m_Screens = new();

        public UIScreen CurrentScreen => m_CurrentScreen;
        public UIDocument Document => m_Document;

        private void OnEnable()
        {
            SubscribeToEvents();

            // Because non-MonoBehaviours can't run coroutines, the Coroutines helper utility allows us to
            // designate a MonoBehaviour to manage starting/stopping coroutines
            Coroutines.Initialize(this);

            Initialize();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            SceneEvents.PreloadCompleted += SceneEvents_PreloadCompleted;

            UIEvents.SplashScreenShown += UIEvents_SplashScreenShown;
            UIEvents.MainMenuShown += UIEvents_MainMenuShown;
            UIEvents.SettingsShown += UIEvents_SettingsShown;
            UIEvents.CharacterSelectionShown += UIEvents_LevelSelectionShown;
            UIEvents.GameScreenShown += UIEvents_GameScreenShown;
            UIEvents.PauseScreenShown += UIEvents_PauseScreenShown;
            UIEvents.ScreenClosed += UIEvents_ScreenClosed;
        }

        private void UnsubscribeFromEvents()
        {
            SceneEvents.PreloadCompleted -= SceneEvents_PreloadCompleted;

            UIEvents.SplashScreenShown -= UIEvents_SplashScreenShown;
            UIEvents.MainMenuShown -= UIEvents_MainMenuShown;
            UIEvents.SettingsShown -= UIEvents_SettingsShown;
            UIEvents.CharacterSelectionShown -= UIEvents_LevelSelectionShown;
            UIEvents.GameScreenShown -= UIEvents_GameScreenShown;
            UIEvents.PauseScreenShown -= UIEvents_PauseScreenShown;
            UIEvents.ScreenClosed -= UIEvents_ScreenClosed;
        }

        private void UIEvents_SplashScreenShown()
        {
            Show(m_SplashScreen, false);
        }

        private void SceneEvents_PreloadCompleted()
        {
            Show(m_StartScreen, false);
        }

        public void UIEvents_MainMenuShown()
        {
            m_CurrentScreen = m_HomeScreen;

            HideScreens();
            m_History.Push(m_HomeScreen);
            m_HomeScreen.Show();
        }

        private void UIEvents_SettingsShown()
        {
            Show(m_SettingsScreen);
        }

        private void UIEvents_LevelSelectionShown()
        {
            Show(m_CharacterSelectionScreen);
        }

        private void UIEvents_GameScreenShown()
        {
            Show(m_GameScreen);
        }

        private void UIEvents_PauseScreenShown()
        {
            Show(m_PauseScreen);
        }

        public void UIEvents_ScreenClosed()
        {
            if (m_History.Count != 0)
            {
                Show(m_History.Pop(), false);
            }
        }

        private void UIEvents_UrlOpened(string link)
        {
            Application.OpenURL(link);
        }

        private void Initialize()
        {
            NullRefChecker.Validate(this);

            VisualElement root = m_Document.rootVisualElement;

            m_SplashScreen = new SplashScreen(root.Q<VisualElement>("splash__container"));
            m_StartScreen = new StartScreen(root.Q<VisualElement>("start__container"));
            m_HomeScreen = new MainMenuScreen(root.Q<VisualElement>("menu__container"));
            // m_SettingsScreen = new SettingsScreen(root.Q<VisualElement>("settings__container"));
            m_CharacterSelectionScreen = new CharacterSelectionScreen(root.Q<VisualElement>("character-select__container"));
            // m_GameScreen = new GameScreen(root.Q<VisualElement>("question-screen__parent"));
            // m_PauseScreen = new PauseScreen(root.Q<VisualElement>("pause__container"));
            // // Notify the GameController the UIScreen for LevelSelection has been setup
            // LevelSelectionEvents.Initialized?.Invoke(m_LevelSelectionScreen as LevelSelectionScreen);

            RegisterScreens();
            HideScreens();
        }

        private void RegisterScreens()
        {
            m_Screens = new List<UIScreen>
            {
                m_SplashScreen,
                m_StartScreen,
                m_HomeScreen
                // m_SettingsScreen,
                // m_CharacterSelectionScreen,
                // m_GameScreen,
                // m_PauseScreen
            };
        }

        private void HideScreens()
        {
            m_History.Clear();

            foreach (UIScreen screen in m_Screens)
            {
                screen.Hide();
            }
        }

        public T GetScreen<T>() where T : UIScreen
        {
            foreach (var screen in m_Screens)
            {
                if (screen is T typeOfScreen)
                {
                    return typeOfScreen;
                }
            }

            return null;
        }

        public void Show<T>(bool keepInHistory = true) where T : UIScreen
        {
            foreach (var screen in m_Screens)
            {
                if (screen is T)
                {
                    Show(screen, keepInHistory);
                    break;
                }
            }
        }

        public void Show(UIScreen screen, bool keepInHistory = true)
        {
            if (screen == null)
                return;

            if (m_CurrentScreen != null)
            {
                if (!screen.IsTransparent)
                    m_CurrentScreen.Hide();

                if (keepInHistory)
                {
                    m_History.Push(m_CurrentScreen);
                }
            }

            screen.Show();
            m_CurrentScreen = screen;
        }

        public void Show(UIScreen screen)
        {
            Show(screen, true);
        }
    }
}