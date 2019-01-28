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

        private CanvasGroup canvasGroup;
        
        private enum States
        {
            Hidden, Visible
        }
        private States state;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            HideCreditsPressed();
            if (RCStatics.Settings != null)
            {
                SensitivitySlider.value = RCStatics.Settings.GetMouseSensitivityFactor();
                EndlessModeToggle.isOn = RCStatics.Settings.EndlessMode;
            }
        }


        public void ExitGamePressed()
        {
            Application.Quit();
        }

        public void ShowCreditsPressed()
        {
            ShowCanvas(CreditsScreenCanvas);
            HideCanvas(MainScreenCanvas);
        }

        public void HideCreditsPressed()
        {
            HideCanvas(CreditsScreenCanvas);
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
            canvas.interactable = false;
            canvas.alpha = 0f;
        }

        private void ShowCanvas(CanvasGroup canvas)
        {
            canvas.interactable = true;
            canvas.alpha = 1f;
        }

        public void SensitivityChanged()
        {
            RCStatics.Settings.SetSensitivity(SensitivitySlider.value);
        }

        public void EndlessModeChanged()
        {
            RCStatics.Settings.EndlessMode = EndlessModeToggle.isOn;
        }
    }
}
