
using System;

namespace rpg.ui
{
    /// <summary>
    /// Public static delegates to manage UI changes (note these are "events" in the conceptual sense
    /// and not the strict C# sense).
    /// </summary>
    public static class UIEvents
    {
        #region Menu/screen events:

        // Close the screen and go back
        public static Action ScreenClosed;

        public static Action SplashScreenShown;

        // Show the Main Menu selection (Settings, Level Selection)
        public static Action MainMenuShown;

        // Show the user settings (sound volume)
        public static Action SettingsShown;

        // Show a level select screen to choose what quiz to play
        public static Action CharacterSelectionShown;

        // Show the main gameplay screen (one formatted question)
        public static Action GameScreenShown;

        // Show a pause screen during gameplay to abort the game
        public static Action PauseScreenShown;

        // Show the results of the quiz
        public static Action EndScreenShown;

        #endregion

        #region Response panel events:

        // User has toggled off all responses (nothing selected)
        public static Action ResponsesDeselected;
        #endregion

        #region Progress bar events:

        // Draw the progress bar's percentage complete
        public static Action<float> ProgressBarUpdated;

        // Show numeric percentage of correct answers
        public static Action<float> AccuracyCalculated;

        #endregion

    }
}
