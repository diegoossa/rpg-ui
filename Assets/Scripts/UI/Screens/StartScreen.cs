using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace rpg.ui
{
    public class StartScreen : UIScreen
    {
        public StartScreen(VisualElement parentElement) : base(parentElement)
        {
            SetVisualElements();
            RegisterCallbacks();
        }

        private void SetVisualElements()
        {
        }

        private void RegisterCallbacks()
        {
            Coroutines.StartCoroutine(WaitForKeyPress());
        }
        
        private IEnumerator WaitForKeyPress()
        {
            while (!Input.anyKeyDown)
            {
                yield return null;
            }
            
            UIEvents.MainMenuShown?.Invoke();
        }
    }
}