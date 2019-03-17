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
            bool success = statics.AssertReferences();
            if (!success)
            {
                return;
            }

            Debug.Log("Starting up game");
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
            StartCoroutine(GameOverRoutine());
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
            StartCoroutine(HighScoreSubmittedRoutine());
        }

        private IEnumerator GameOverRoutine()
        {
            state = States.Menu;

            const float TIME_SLOW_DURATION = 1f;
            float timer = 0f;
            while (timer < TIME_SLOW_DURATION)
            {
                timer += Time.unscaledDeltaTime;
                Time.timeScale = Mathf.Lerp(1f, 0f, timer / TIME_SLOW_DURATION);
                yield return null;
            }

            int score = RCStatics.Score.GetScore();
            if (RCStatics.Data.IsNewHighscore(score))
            {
                RCStatics.UI.HighScore.ShowNewScore(score);
                // Show new score will provide a submit option
            }
            else
            {
                TransistionController.instance.GameOut();
            }
        }

        private IEnumerator HighScoreSubmittedRoutine()
        {
            yield return StartCoroutine(RCStatics.UI.HighScore.FadeOutRoutine());
            TransistionController.instance.GameOut();
        }

    }
}


