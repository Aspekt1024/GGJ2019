using System.Collections;
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
            int score = RCStatics.Score.GetScore();
            if (RCStatics.Data.IsNewHighscore(score))
            {
                Time.timeScale = 0f;
                state = States.Menu;
                RCStatics.UI.HighScore.ShowNewScore(score);
                // Show new score will provide a submit option
            }
            else
            {
                TransistionController.instance.gameOut();
            }
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

        public void NewScoreSubmitted()
        {
            StartCoroutine(GameoverRoutine());
        }

        private IEnumerator GameoverRoutine()
        {
            yield return StartCoroutine(RCStatics.UI.HighScore.FadeOutRoutine());
            Time.timeScale = 1f;
            state = States.InGame;
            TransistionController.instance.gameOut();
        }
    }
}


