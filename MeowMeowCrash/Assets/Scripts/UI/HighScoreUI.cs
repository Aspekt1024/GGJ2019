using RobotCat.Data;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace RobotCat.UI
{
    public class HighScoreUI : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private HighScoreScrollView scoresView;
        [SerializeField] private Button backButton;
        [SerializeField] private Button submitButton;
        #pragma warning restore 649

        public void PopulateUI(List<ScoreItem> scores)
        {
            scoresView.Reset();
            scoresView.SetContentSize(scores.Count);
            for (int i = 0; i < scores.Count; i++)
            {
                if (scores[i].name == "")
                {
                    scoresView.AddScoreWithNameInput(i + 1, scores[i].score);
                }
                else
                {
                    scoresView.AddScore(i + 1, scores[i].name, scores[i].score);
                }
            }
            backButton.gameObject.SetActive(true);
            submitButton.gameObject.SetActive(false);
        }

        public void ShowNewScore(int score)
        {
            RCStatics.UI.ToggleMenu();
            RCStatics.UI.Menu.ShowHighScoresWithoutPopulating();

            var scores = RCStatics.Data.GetScores();
            scores.Add(new ScoreItem("", score));
            scores = scores.OrderByDescending(x => x.score).ToList();

            PopulateUI(scores);
            submitButton.gameObject.SetActive(true);
            backButton.gameObject.SetActive(false);
        }

        public void SubmitClicked()
        {
            scoresView.HandleSubmit();
        }

        public void HideButtons()
        {
            backButton.gameObject.SetActive(false);
            submitButton.gameObject.SetActive(false);
        }
    }
}
