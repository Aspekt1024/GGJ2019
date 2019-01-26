using UnityEngine;

namespace RobotCat.UI
{
    public class MenuScripts : MonoBehaviour
    {

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
            canvasGroup.interactable = false;
            canvasGroup.alpha = 0f;
            state = States.Hidden;
        }

        public void Show()
        {
            canvasGroup.interactable = true;
            canvasGroup.alpha = 1f;
            state = States.Visible;
        }
    }
}
