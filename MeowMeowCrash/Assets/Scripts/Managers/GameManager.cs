using UnityEngine;

namespace RobotCat
{
    public class GameManager : MonoBehaviour
    {
        private RCStatics statics;

        private enum States
        {
            InGame, Menu
        }
        private States state = States.InGame;

        private void Awake()
        {
            statics = new RCStatics(this);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Start()
        {
            RCStatics.UI.HideMenu();
            statics.OnStart();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Tab))
            {
                if (state == States.Menu)
                {
                    HideMenu();
                }
                else
                {
                    ShowMenu();
                }
            }
        }

        public void GameOver()
        {
            RCStatics.Data.AddScore("You (yes you!)", RCStatics.Score.GetScore());
            RCStatics.Data.SaveData();
            TransistionController.instance.gameOut();
        }

        public void ShowMenu()
        {
            state = States.Menu;
            RCStatics.UI.Menu.Show();
            Time.timeScale = 0f;
        }

        public void HideMenu()
        {
            state = States.InGame;
            RCStatics.UI.Menu.Hide();
            Time.timeScale = 1f;
        }

        public bool IsInMenu { get { return state == States.Menu; } }
    }
}


