using UnityEngine;

namespace RobotCat.UI
{
    public class MenuScripts : MonoBehaviour
    {
        public CanvasGroup buttons;
        private CanvasGroup canvasGroup;

        private enum States
        {
            Hidden, Visible
        }
        private States state;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ExitGamePressed()
        {
            Application.Quit();
        }

        public void ShowCreditsPressed()
        {
            RCStatics.UI.ShowCredits();
            HideCanvas(buttons);
        }

        public void HideCreditsPressed()
        {
            RCStatics.UI.HideCredits();
            ShowCanvas(buttons);
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
    }
}
