using RobotCat.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RobotCat.UI
{
    public class HighScoreUI : MonoBehaviour
    {
        #pragma warning disable 649
        [SerializeField] private TextMeshProUGUI numText;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI scoreText;
        #pragma warning restore 649

        public void PopulateUI(List<ScoreItem> scores)
        {
            string numString = "";
            string nameString = "";
            string scoreString = "";
            for (int i = 0; i < scores.Count; i++)
            {
                numString += (i + 1).ToString() + ".\n";
                nameString += scores[i].Name + "\n";
                scoreString += scores[i].Score.ToString() + "\n";
            }

            numText.text = numString;
            nameText.text = nameString;
            scoreText.text = scoreString;
        }
    }
}
