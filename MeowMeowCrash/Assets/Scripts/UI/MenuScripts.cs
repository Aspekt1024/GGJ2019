using UnityEngine;
using UnityEngine.UI;

namespace RobotCat.UI
{
    public class MenuScripts : MonoBehaviour
    {
        public Slider SensitivitySlider;
        public Toggle EndlessModeToggle;

        public CanvasGroup MainScreenCanvas;
        public CanvasGroup CreditsScreenCanvas;
        public CanvasGroup HighScoreCanvas;

        private CanvasGroup canvasGroup;
        
        private enum States
        {
            Hidden, Visible
        }
        private States state;

        private void Start()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            HideCreditsPressed();
        }

        public void InitializeValues(SettingsManager settings)
        {
            SensitivitySlider.value = settings.GetMouseSensitivityFactor();
            EndlessModeToggle.isOn = settings.EndlessMode;
        }

        public void ExitGamePressed()
        {
            Application.Quit();
        }

        public void ShowCreditsPressed()
        {
            ShowCanvas(CreditsScreenCanvas);
            HideCanvas(MainScreenCanvas);
            HideCanvas(HighScoreCanvas);
        }

        public void HideCreditsPressed()
        {
            HideCanvas(CreditsScreenCanvas);
            HideCanvas(HighScoreCanvas);
            ShowCanvas(MainScreenCanvas);
        }

        public void ShowHighScorePressed()
        {
            ShowHighScoresWithoutPopulating();
            RCStatics.UI.HighScore.PopulateUI(RCStatics.Data.GetScores());
        }

        public void ShowHighScoresWithoutPopulating()
        {
            HideCanvas(CreditsScreenCanvas);
            HideCanvas(MainScreenCanvas);
            ShowCanvas(HighScoreCanvas);
        }

        public void HideHighScorePressed()
        {
            HideCanvas(CreditsScreenCanvas);
            HideCanvas(HighScoreCanvas);
            ShowCanvas(MainScreenCanvas);
        }

        public void ReturnToGamePressed()
        {
            RCStatics.GameManager.HideMenu();
        }
        
        public void Toggle()
        {
            if (state == States.Hidden)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Hide()
        {
            state = States.Hidden;
            HideCanvas(canvasGroup);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void Show()
        {
            state = States.Visible;
            ShowCanvas(canvasGroup);

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        private void HideCanvas(CanvasGroup canvas)
        {
            canvas.blocksRaycasts = false;
            canvas.interactable = false;
            canvas.alpha = 0f;
        }

        private void ShowCanvas(CanvasGroup canvas)
        {
            canvas.blocksRaycasts = true;
            canvas.interactable = true;
            canvas.alpha = 1f;
        }

        public void SensitivityChanged()
        {
            if (RCStatics.Settings == null) return;
            RCStatics.Settings.SetSensitivity(SensitivitySlider.value);
        }

        public void EndlessModeChanged()
        {
            if (RCStatics.Settings == null) return;
            RCStatics.Settings.EndlessMode = EndlessModeToggle.isOn;
        }
    }
}
